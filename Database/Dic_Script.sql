CREATE TABLE CONFIGURATION (
ID INT IDENTITY (1,1) NOT NULL,
ID_PERIOD INT NOT NULL,
CONSTRAINT PK_CONFIGURATION PRIMARY KEY CLUSTERED (ID))

CREATE TABLE PERIOD (
ID INT IDENTITY (1,1) NOT NULL,
MONTHS INT NOT NULL,
NAME VARCHAR(25) NOT NULL,
REMOVED BIT DEFAULT 0,
CONSTRAINT PK_PERIOD PRIMARY KEY CLUSTERED (ID))

CREATE TABLE DIC_HISTORY (
ID INT IDENTITY (1,1) NOT NULL,
ID_DIC INT NOT NULL,
ID_STATUS_DIC INT NOT NULL,
NOTE VARCHAR(MAX) NOT NULL,
DATE  DATETIME DEFAULT(GETDATE()),
CONSTRAINT PK_DIC_HISTORY PRIMARY KEY CLUSTERED (ID))

CREATE TABLE DIC (
ID INT IDENTITY (1,1) NOT NULL,
ID_USER INT NOT NULL,
ID_STATUS INT NOT NULL,
ID_PERIOD INT NOT NULL,
DESCRIPTION VARCHAR(MAX) NOT NULL,
START_DATE DATETIME NOT NULL,
END_DATE DATETIME NOT NULL,
FINISHED_DATE DATETIME NOT NULL,
CONSTRAINT PK_DIC PRIMARY KEY CLUSTERED (ID))

CREATE TABLE STATUS (
ID INT IDENTITY (1,1) NOT NULL,
NAME VARCHAR(50) NOT NULL,
REMOVED BIT DEFAULT 0,
CONSTRAINT PK_STATUS PRIMARY KEY CLUSTERED (ID))

CREATE TABLE PROCESS (
ID INT IDENTITY (1,1) NOT NULL,
ID_DEPARTMENT INT NOT NULL,
NAME VARCHAR(50) NOT NULL,
REMOVED BIT DEFAULT 0,
CONSTRAINT PK_PROCESS PRIMARY KEY CLUSTERED (ID))

CREATE TABLE DEPARTMENT (
ID INT IDENTITY (1,1) NOT NULL, 
NAME VARCHAR(50) NOT NULL,
REMOVED BIT DEFAULT 0,
CONSTRAINT PK_DEPARTMENT PRIMARY KEY CLUSTERED (ID))

CREATE TABLE USERS (
ID INT IDENTITY (1,1) NOT NULL,
ID_DEPARTMENT INT NOT NULL,
ID_PROCESS INT NOT NULL,
NAME VARCHAR(50) NOT NULL,
AVATAR VARCHAR(MAX),
EMAIL VARCHAR(50) NOT NULL,
PWD VARCHAR(MAX) NOT NULL,
IS_LEADER_DEPARTMENT BIT DEFAULT 0,
IS_LEADER_PROCESS BIT DEFAULT 0,
REMOVED BIT DEFAULT 0,
CONSTRAINT PK_USER PRIMARY KEY CLUSTERED (ID))

/*FOREIGN KEYS*/
ALTER TABLE CONFIGURATION WITH CHECK ADD CONSTRAINT FK_PERIOD
FOREIGN KEY (ID_PERIOD) REFERENCES PERIOD (ID)

ALTER TABLE DIC_HISTORY WITH CHECK ADD CONSTRAINT FK_DIC
FOREIGN KEY (ID_DIC) REFERENCES DIC (ID)

ALTER TABLE DIC_HISTORY WITH CHECK ADD CONSTRAINT FK_STATUS
FOREIGN KEY (ID_STATUS_DIC) REFERENCES STATUS (ID)

ALTER TABLE DIC WITH CHECK ADD CONSTRAINT FK_USERS
FOREIGN KEY (ID_USER) REFERENCES USERS (ID)

ALTER TABLE DIC WITH CHECK ADD CONSTRAINT FK_DIC_STATUS
FOREIGN KEY (ID_STATUS) REFERENCES STATUS (ID)

ALTER TABLE PROCESS WITH CHECK ADD CONSTRAINT FK_DEPARTMENT
FOREIGN KEY (ID_DEPARTMENT) REFERENCES DEPARTMENT (ID)

ALTER TABLE USERS WITH CHECK ADD CONSTRAINT FK_USER_DEPARTMENT
FOREIGN KEY (ID_DEPARTMENT) REFERENCES DEPARTMENT (ID)

ALTER TABLE USERS WITH CHECK ADD CONSTRAINT FK_USER_PROCESS
FOREIGN KEY (ID_PROCESS) REFERENCES PROCESS (ID)

ALTER TABLE DIC WITH CHECK ADD CONSTRAINT FK_DIC_PERIOD
FOREIGN KEY (ID_PERIOD) REFERENCES PERIOD (ID)
