/**
 * Keil project example for MPU6050 device with interrupts capability
 *
 * Before you start, select your target, on the right of the "Load" button
 *
 * @author    Tilen Majerle
 * @email     tilen@majerle.eu
 * @website   http://stm32f4-discovery.net
 * @ide       Keil uVision 5
 * @conf      PLL parameters are set in "Options for Target" -> "C/C++" -> "Defines"
 * @packs     STM32F4xx/STM32F7xx Keil packs are requred with HAL driver support
 * @stdperiph STM32F4xx/STM32F7xx HAL drivers required
 */
/* Include core modules */
#include "stm32fxxx_hal.h"
/* Include my libraries here */
#include "defines.h"
#include "tm_stm32_disco.h"
#include "tm_stm32_delay.h"
#include "tm_stm32_fatfs.h"
#include "tm_stm32_mpu6050.h"
#include "tm_stm32_mpu6050_dmp.h"
#include "tm_stm32_mpu6050_dmp_6axis_MotionApps20.h"
#include "tm_stm32_usart.h"
/* FATFS */
FATFS FS;
FIL fil;
FRESULT fres;	
TM_FATFS_Size_t CardSize;
char bufferread[100];
UINT byteread=0;
char buffer[100];
uint16_t dataco=0;  
uint8_t write=0;
char filename[30];
uint8_t filenumber=0;
/* END FATFS */




/* MPU6050 working structure */
TM_MPU6050_t MPU6050;

/* Interrupts structure */
TM_MPU6050_Interrupt_t MPU6050_Interrupts;

/* Define GPIO PORT and PIN for interrupt handling */
#define IRQ_PORT    GPIOA
#define IRQ_PIN     GPIO_PIN_1

/* Flag when we should read */
uint32_t read = 0;
uint8_t devStatus;
uint8_t mpuIntStatus; 
bool dmpReady = false;
uint16_t packetSize;


Quaternion_t q; 
VectorFloat_t gravity;
float ypr[3]; 

float Yaw,Pitch,Roll;
volatile bool mpuInterrupt = false;     // indicates whether MPU interrupt pin has gone high
void dmpDataReady() {
    mpuInterrupt = true;
}
   
uint16_t fifoCount;    
uint8_t fifoBuffer[64];
uint8_t buff_char[50];
uint8_t k=0;
int main(void) {
	
	/* Init system clock for maximum system speed */
	TM_RCC_InitSystem();
	
	/* Init HAL layer */
	HAL_Init();
	
	/* Init leds */
	TM_DISCO_LedInit();
//	/* FATFS */
//	if (f_mount(&FS, "SD:", 1) == FR_OK) {
//		/* Try to open file */
//		if ((fres = f_open(&fil, "SD:first_file.txt",FA_CREATE_ALWAYS|FA_READ | FA_WRITE)) == FR_OK) {
//			/* Read SDCARD size */
//		//	TM_FATFS_GetDriveSize("SD:", &CardSize);
//		//	sprintf(buffer, "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789\n");
//		//	f_puts(buffer, &fil);
//		//	f_close(&fil);
//			
//			
//			/* Format string */
//			//sprintf(buffer, "Total card size: %u kBytes\n", CardSize.Total);
//			
//			/* Write total card size to file */
//			//f_puts(buffer, &fil);
//			
//			/* Format string for free card size */
//			//sprintf(buffer, "Free card size:  %u kBytes\n", CardSize.Free);
//			
//			/* Write free card size to file */
//			//f_puts(buffer, &fil);
//			for(uint16_t i =0;i<5000;i++)
//			{
//			//fres=f_open(&fil, "first_file.txt", FA_READ|FA_WRITE);
//			sprintf(buffer, "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789\n");
//			f_puts(buffer, &fil);
//			//f_read(&fil,bufferread,99,&byteread);
//			//f_close(&fil);
//			}
//			/* Close file */
//			f_close(&fil);
//			/* Turn led ON */
//			//TM_DISCO_LedOn(LED_ALL);
//		}
//		
//		/* Unmount SDCARD */
//		f_mount(NULL, "SD:", 1);
//	}

//	
//	/* END FATFS */
	
	/* MPU6050 */
	
	/* Init button */
	TM_DISCO_ButtonInit();
	//---------------
	TM_I2C_Init(MPU6050_I2C, MPU6050_I2C_PINSPACK, MPU6050_I2C_CLOCK);
	TM_MPU6050_Init(&MPU6050, TM_MPU6050_Device_0, TM_MPU6050_Accelerometer_8G, TM_MPU6050_Gyroscope_250s);
	
	MPU6050address(0xD0);
	MPU6050_initialize();
	TM_EXTI_Attach(IRQ_PORT, IRQ_PIN, TM_EXTI_Trigger_Rising);
	devStatus = MPU6050_dmpInitialize();
	TM_MPU6050_ReadAll(&MPU6050);
	MPU6050_setXGyroOffset(-27);
	MPU6050_setYGyroOffset(-1);
	MPU6050_setZGyroOffset(-28);
	//MPU6050_setXAccelOffset(MPU6050.Accelerometer_X);
	//MPU6050_setYAccelOffset(MPU6050.Accelerometer_Y);
	//MPU6050_setZAccelOffset(MPU6050.Accelerometer_Z);
	TM_USART_Init(USART1, TM_USART_PinsPack_1, 921600);
	TM_USART_Puts(USART1, "Hello world\n");
	if (devStatus == 0) {
		MPU6050_setDMPEnabled(true);
		mpuIntStatus = MPU6050_getIntStatus();
		dmpReady = true;
		packetSize = MPU6050_dmpGetFIFOPacketSize();
	}
	/*FATFS*/
	if (f_mount(&FS, "SD:", 1) == FR_OK) {
		sprintf(filename,"SD:%d.txt",filenumber);
		/* Try to open file */
		if ((fres = f_open(&fil,filename,FA_CREATE_ALWAYS|FA_READ | FA_WRITE)) == FR_OK) {
			write =1;
		}
	}
	/*END FATFS*/
	while(1)
	{
		while (!mpuInterrupt && fifoCount < packetSize) ;
		mpuInterrupt = false;
		mpuIntStatus = MPU6050_getIntStatus();
		fifoCount = MPU6050_getFIFOCount();
		if ((mpuIntStatus & 0x10) || fifoCount == 1024) {
        // reset so we can continue cleanly
        MPU6050_resetFIFO();
			}else if (mpuIntStatus & 0x02) {
        // wait for correct available data length, should be a VERY short wait
        while (fifoCount < packetSize) fifoCount = MPU6050_getFIFOCount();

        // read a packet from FIFO
        MPU6050_getFIFOBytes(fifoBuffer, packetSize);
        TM_MPU6050_ReadAll(&MPU6050);
        // track FIFO count here in case there is > 1 packet available
        // (this lets us immediately read more without waiting for an interrupt)
        fifoCount -= packetSize;
				
				MPU6050_dmpGetQuaternion(&q, fifoBuffer);
        MPU6050_dmpGetGravity(&gravity, &q);
        MPU6050_dmpGetYawPitchRoll(ypr, &q, &gravity);
				Yaw=ypr[0]*180/3.14;
				Pitch=ypr[1]*180/3.14;
				Roll=ypr[2]*180/3.14;
				k++;
				if(k==10)
					{k=0;
					sprintf((char*)buff_char,"%0.5f\t%0.5f\t%0.5f\t%d\t%d\t%d\t%d\t%d\t%d\r\n",Yaw,Pitch,Roll,MPU6050.Accelerometer_X,MPU6050.Accelerometer_Y,MPU6050.Accelerometer_Z,MPU6050.Gyroscope_X,MPU6050.Gyroscope_Y,MPU6050.Gyroscope_Z);
					TM_USART_Puts(USART1,(char*)buff_char);
						if(write==1)
						{
							f_puts((const char *)buff_char, &fil);
							dataco++;
							if(dataco==500)
							{
								dataco=0;
								f_close(&fil);
								filenumber++;
								sprintf(filename,"SD:%d.txt",filenumber);
								if ((fres = f_open(&fil,filename,FA_CREATE_ALWAYS|FA_READ | FA_WRITE)) == FR_OK) {
									write =1;
								}
							}
						}
							
					}
				}
	}
	/*END FATFS	*/
	//_----------------------
	/* For pinouts check TM_MPU6050 library */
	
	/* Try to init MPU6050, device address is 0xD0, AD0 pin is set to low */
	
//	if (TM_MPU6050_Init(&MPU6050, TM_MPU6050_Device_0, TM_MPU6050_Accelerometer_8G, TM_MPU6050_Gyroscope_250s) == TM_MPU6050_Result_Ok) {
//		/* Green LED on */
//		TM_DISCO_LedOn(LED_GREEN);
//	}
//	
//	/* Set data rate to 100 Hz */
//	TM_MPU6050_SetDataRate(&MPU6050, TM_MPU6050_DataRate_100Hz);
//	
//	/* Enable MPU interrupts */
//	TM_MPU6050_EnableInterrupts(&MPU6050);
//	
//	/* Enable interrupts on STM32Fxxx device, rising edge */
//	TM_EXTI_Attach(IRQ_PORT, IRQ_PIN, TM_EXTI_Trigger_Rising);
//	
//	while (1) {
//		/* If IRQ happen */
//		if (read) {
//			/* Reset */
//			read = 0;
//			
//			/* Read interrupts */
//			TM_MPU6050_ReadInterrupts(&MPU6050, &MPU6050_Interrupts);
//			
//			/* Check if motion is detected */
//			if (MPU6050_Interrupts.F.MotionDetection) {
//				/* Toggle RED */
//				TM_DISCO_LedToggle(LED_RED);
//			}
//			
//			/* Check if data ready */
//			if (MPU6050_Interrupts.F.DataReady) {
//				/* Read everything from device */
//				TM_MPU6050_ReadAll(&MPU6050);
//				
//				/* Raw data are available for use when needed */
//				//MPU6050.Accelerometer_X;
//				//MPU6050.Accelerometer_Y;
//				//MPU6050.Accelerometer_Z;
//				//MPU6050.Gyroscope_X;
//				//MPU6050.Gyroscope_Y;
//				//MPU6050.Gyroscope_Z;
//				//MPU6050.Temperature;
//			}
//		}
//	}
}

/* EXTI handler */
void TM_EXTI_Handler(uint16_t GPIO_Pin) {
	/* Check for PIN */
	if (GPIO_Pin == IRQ_PIN) {
		/* Read interrupts from MPU6050 */
		read = 1;
		dmpDataReady();
	}
}

