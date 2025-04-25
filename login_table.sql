﻿CREATE TABLE `login_table` (
	`LoginID` INT(11) NOT NULL AUTO_INCREMENT,
	`LoginUsername` VARCHAR(255) NOT NULL DEFAULT '0' COLLATE 'utf8mb4_uca1400_ai_ci',
	`LoginPassword` VARCHAR(255) NOT NULL DEFAULT '0' COLLATE 'utf8mb4_uca1400_ai_ci',
	PRIMARY KEY (`LoginID`) USING BTREE
)
COLLATE='utf8mb4_uca1400_ai_ci'
ENGINE=InnoDB
AUTO_INCREMENT=3
;
