SET PATH=F:\Keil_v5\ARM\ARMCC\Bin;C:\Program Files (x86)\NVIDIA Corporation\PhysX\Common;C:\Program Files (x86)\STMicroelectronics\st_toolset\asm;C:\Python27\;C:\Python27\Scripts;C:\ProgramData\Oracle\Java\javapath;C:\Windows\system32;C:\Windows;C:\Windows\System32\Wbem;C:\Windows\System32\WindowsPowerShell\v1.0\;C:\Program Files\Git\cmd;C:\Windows\system32\config\systemprofile\.dnx\bin;C:\Program Files\Microsoft DNX\Dnvm\;C:\Program Files\Microsoft SQL Server\130\Tools\Binn\;C:\Program Files\PuTTY\;C:\Program Files\MySQL\MySQL Utilities 1.6\
SET ARMCC5_ASMOPT=--diag_suppress=9931
SET ARMCC5_CCOPT=--diag_suppress=9931
SET ARMCC5_LINKOPT=--diag_suppress=9931
SET CPU_TYPE=STM32F103C8
SET CPU_VENDOR=STMicroelectronics
SET UV2_TARGET=STM32F1_MPU6050
SET CPU_CLOCK=0x044AA200
"F:\Keil_v5\ARM\ARMCC\Bin\ArmAsm" --Via "stm32f1_mpu6050\startup_stm32f103xb._ia"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\mpu6050.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\mpu6050_dmp.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\mpu6050_dmp_6axis_motionapps20.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\main.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_it.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_msp.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_i2c.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_dma.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_rcc.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_rcc_ex.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_cortex.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_gpio.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_gpio_ex.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_uart.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_tim_ex.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_flash.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_flash_ex.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_tim.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\stm32f1xx_hal_pwr.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmCC" --Via "stm32f1_mpu6050\system_stm32f1xx.__i"
"F:\Keil_v5\ARM\ARMCC\Bin\ArmLink" --Via "STM32F1_MPU6050\STM32F1_MPU6050.lnp"
"F:\Keil_v5\ARM\ARMCC\Bin\fromelf.exe" "STM32F1_MPU6050\STM32F1_MPU6050.axf" --i32combined --output "STM32F1_MPU6050\STM32F1_MPU6050.hex"
