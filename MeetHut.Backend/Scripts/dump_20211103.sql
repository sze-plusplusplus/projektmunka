CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
    ) CHARACTER SET utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Users` (
                         `Id` int NOT NULL AUTO_INCREMENT,
                         `UserName` varchar(120) CHARACTER SET utf8mb4 NOT NULL,
                         `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
                         `FullName` longtext CHARACTER SET utf8mb4 NULL,
                         `PasswordHash` longtext CHARACTER SET utf8mb4 NOT NULL,
                         `LastLogin` datetime(6) NOT NULL,
                         `Role` int NOT NULL DEFAULT 3,
                         `Creation` datetime(6) NOT NULL,
                         `LastUpdate` datetime(6) NOT NULL,
                         CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Rooms` (
                         `Id` int NOT NULL AUTO_INCREMENT,
                         `Name` longtext CHARACTER SET utf8mb4 NULL,
                         `PublicId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                         `OwnerId` int NOT NULL,
                         `StartTime` datetime(6) NULL,
                         `EndTime` datetime(6) NULL,
                         `Locked` tinyint(1) NOT NULL,
                         `Creation` datetime(6) NOT NULL,
                         `LastUpdate` datetime(6) NOT NULL,
                         CONSTRAINT `PK_Rooms` PRIMARY KEY (`Id`),
                         CONSTRAINT `FK_Rooms_Users_OwnerId` FOREIGN KEY (`OwnerId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

CREATE TABLE `RoomUsers` (
                             `UserId` int NOT NULL,
                             `RoomId` int NOT NULL,
                             `Role` int NOT NULL DEFAULT 4,
                             `Added` datetime(6) NOT NULL DEFAULT NOW(),
                             `AdderId` int NOT NULL,
                             CONSTRAINT `PK_RoomUsers` PRIMARY KEY (`RoomId`, `UserId`),
                             CONSTRAINT `FK_RoomUsers_Rooms_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `Rooms` (`Id`) ON DELETE RESTRICT,
                             CONSTRAINT `FK_RoomUsers_Users_AdderId` FOREIGN KEY (`AdderId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT,
                             CONSTRAINT `FK_RoomUsers_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

CREATE INDEX `IX_Rooms_OwnerId` ON `Rooms` (`OwnerId`);

CREATE UNIQUE INDEX `IX_Rooms_PublicId` ON `Rooms` (`PublicId`);

CREATE INDEX `IX_RoomUsers_AdderId` ON `RoomUsers` (`AdderId`);

CREATE INDEX `IX_RoomUsers_UserId` ON `RoomUsers` (`UserId`);

CREATE UNIQUE INDEX `IX_Users_UserName` ON `Users` (`UserName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211009101924_Init', '5.0.9');

COMMIT;

START TRANSACTION;

ALTER TABLE `Users` MODIFY COLUMN `Email` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

CREATE UNIQUE INDEX `IX_Users_Email` ON `Users` (`Email`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211010125745_Auth', '5.0.9');

COMMIT;

START TRANSACTION;

ALTER TABLE `Users` ADD `RefreshToken` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Users` ADD `RefreshTokenExpiryTime` datetime(6) NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211012191820_Auth2', '5.0.9');

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
        DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
        DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
        DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
        DECLARE SQL_EXP VARCHAR(1000);
SELECT COUNT(*)
INTO HAS_AUTO_INCREMENT_ID
FROM `information_schema`.`COLUMNS`
WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
  AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
  AND `Extra` = 'auto_increment'
  AND `COLUMN_KEY` = 'PRI'
    LIMIT 1;
IF HAS_AUTO_INCREMENT_ID THEN
SELECT `COLUMN_TYPE`
INTO PRIMARY_KEY_TYPE
FROM `information_schema`.`COLUMNS`
WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
  AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
  AND `COLUMN_KEY` = 'PRI'
    LIMIT 1;
SELECT `COLUMN_NAME`
INTO PRIMARY_KEY_COLUMN_NAME
FROM `information_schema`.`COLUMNS`
WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
  AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
  AND `COLUMN_KEY` = 'PRI'
    LIMIT 1;
SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
                SET @SQL_EXP = SQL_EXP;
PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
EXECUTE SQL_EXP_EXECUTE;
DEALLOCATE PREPARE SQL_EXP_EXECUTE;
END IF;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
        DECLARE HAS_AUTO_INCREMENT_ID INT(11);
        DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
        DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
        DECLARE SQL_EXP VARCHAR(1000);
SELECT COUNT(*)
INTO HAS_AUTO_INCREMENT_ID
FROM `information_schema`.`COLUMNS`
WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
  AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
  AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
  AND `COLUMN_TYPE` LIKE '%int%'
  AND `COLUMN_KEY` = 'PRI';
IF HAS_AUTO_INCREMENT_ID THEN
SELECT `COLUMN_TYPE`
INTO PRIMARY_KEY_TYPE
FROM `information_schema`.`COLUMNS`
WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
  AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
  AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
  AND `COLUMN_TYPE` LIKE '%int%'
  AND `COLUMN_KEY` = 'PRI';
SELECT `COLUMN_NAME`
INTO PRIMARY_KEY_COLUMN_NAME
FROM `information_schema`.`COLUMNS`
WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
  AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
  AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
  AND `COLUMN_TYPE` LIKE '%int%'
  AND `COLUMN_KEY` = 'PRI';
SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
                SET @SQL_EXP = SQL_EXP;
PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
EXECUTE SQL_EXP_EXECUTE;
DEALLOCATE PREPARE SQL_EXP_EXECUTE;
END IF;
END //
DELIMITER ;

ALTER TABLE `Rooms` DROP FOREIGN KEY `FK_Rooms_Users_OwnerId`;

ALTER TABLE `RoomUsers` DROP FOREIGN KEY `FK_RoomUsers_Users_AdderId`;

ALTER TABLE `RoomUsers` DROP FOREIGN KEY `FK_RoomUsers_Users_UserId`;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'Users');
ALTER TABLE `Users` DROP PRIMARY KEY;

ALTER TABLE `Users` DROP COLUMN `Creation`;

ALTER TABLE `Users` DROP COLUMN `LastUpdate`;

ALTER TABLE `Users` RENAME `AspNetUsers`;

ALTER TABLE `AspNetUsers` DROP INDEX `IX_Users_UserName`;

CREATE UNIQUE INDEX `IX_AspNetUsers_UserName` ON `AspNetUsers` (`UserName`);

ALTER TABLE `AspNetUsers` DROP INDEX `IX_Users_Email`;

CREATE UNIQUE INDEX `IX_AspNetUsers_Email` ON `AspNetUsers` (`Email`);

ALTER TABLE `AspNetUsers` MODIFY COLUMN `UserName` varchar(256) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AspNetUsers` MODIFY COLUMN `PasswordHash` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AspNetUsers` MODIFY COLUMN `Email` varchar(256) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AspNetUsers` ADD `AccessFailedCount` int NOT NULL DEFAULT 0;

ALTER TABLE `AspNetUsers` ADD `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AspNetUsers` ADD `EmailConfirmed` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `AspNetUsers` ADD `LockoutEnabled` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `AspNetUsers` ADD `LockoutEnd` datetime(6) NULL;

ALTER TABLE `AspNetUsers` ADD `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AspNetUsers` ADD `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AspNetUsers` ADD `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AspNetUsers` ADD `PhoneNumberConfirmed` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `AspNetUsers` ADD `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AspNetUsers` ADD `TwoFactorEnabled` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `AspNetUsers` ADD CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'AspNetUsers', 'Id');

CREATE TABLE `AspNetRoles` (
                               `Id` int NOT NULL AUTO_INCREMENT,
                               `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
                               `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
                               `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
                               CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetUserClaims` (
                                    `Id` int NOT NULL AUTO_INCREMENT,
                                    `UserId` int NOT NULL,
                                    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
                                    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
                                    CONSTRAINT `PK_AspNetUserClaims` PRIMARY KEY (`Id`),
                                    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetUserLogins` (
                                    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
                                    `UserId` int NOT NULL,
                                    CONSTRAINT `PK_AspNetUserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
                                    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetUserTokens` (
                                    `UserId` int NOT NULL,
                                    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                    `Value` longtext CHARACTER SET utf8mb4 NULL,
                                    CONSTRAINT `PK_AspNetUserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
                                    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetRoleClaims` (
                                    `Id` int NOT NULL AUTO_INCREMENT,
                                    `RoleId` int NOT NULL,
                                    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
                                    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
                                    CONSTRAINT `PK_AspNetRoleClaims` PRIMARY KEY (`Id`),
                                    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE `AspNetUserRoles` (
                                   `UserId` int NOT NULL,
                                   `RoleId` int NOT NULL,
                                   CONSTRAINT `PK_AspNetUserRoles` PRIMARY KEY (`UserId`, `RoleId`),
                                   CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
                                   CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

ALTER TABLE `Rooms` ADD CONSTRAINT `FK_Rooms_AspNetUsers_OwnerId` FOREIGN KEY (`OwnerId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `RoomUsers` ADD CONSTRAINT `FK_RoomUsers_AspNetUsers_AdderId` FOREIGN KEY (`AdderId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `RoomUsers` ADD CONSTRAINT `FK_RoomUsers_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT;

DROP PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`;

DROP PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211018194614_Identity', '5.0.9');

COMMIT;

START TRANSACTION;

ALTER TABLE `AspNetUsers` DROP COLUMN `Role`;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211018205037_Identity2', '5.0.9');

COMMIT;

START TRANSACTION;

ALTER TABLE `AspNetUsers` DROP COLUMN `RefreshToken`;

ALTER TABLE `AspNetUsers` DROP COLUMN `RefreshTokenExpiryTime`;

CREATE TABLE `RefreshTokens` (
                                 `Id` int NOT NULL AUTO_INCREMENT,
                                 `Token` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
                                 `Expires` datetime(6) NOT NULL,
                                 `Created` datetime(6) NOT NULL,
                                 `Revoked` datetime(6) NULL,
                                 `UserId` int NOT NULL,
                                 CONSTRAINT `PK_RefreshTokens` PRIMARY KEY (`Id`),
                                 CONSTRAINT `FK_RefreshTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

CREATE UNIQUE INDEX `IX_RefreshTokens_Token` ON `RefreshTokens` (`Token`);

CREATE INDEX `IX_RefreshTokens_UserId` ON `RefreshTokens` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20211019192646_Identity3', '5.0.9');

COMMIT;