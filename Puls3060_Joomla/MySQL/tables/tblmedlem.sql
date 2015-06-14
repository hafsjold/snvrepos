USE puls3060_dk;

CREATE TABLE puls3060_dk.tblmedlem (
  Nr int(11) NOT NULL,
  Kon varchar(1) DEFAULT NULL,
  FodtDato datetime DEFAULT NULL,
  Navn varchar(35) DEFAULT NULL,
  Kaldenavn varchar(25) DEFAULT NULL,
  Adresse varchar(70) DEFAULT NULL,
  Postnr varchar(4) DEFAULT NULL,
  Bynavn varchar(25) DEFAULT NULL,
  Telefon varchar(8) DEFAULT NULL,
  Email varchar(50) DEFAULT NULL,
  Bank varchar(15) DEFAULT NULL,
  Status int(11) DEFAULT NULL,
  a_Adresse varchar(255) DEFAULT NULL,
  a_Bynavn varchar(255) DEFAULT NULL,
  user_id int(11) DEFAULT NULL,
  PRIMARY KEY (Nr),
  UNIQUE INDEX Nr (Nr)
)
ENGINE = INNODB
AVG_ROW_LENGTH = 214
CHARACTER SET utf8
COLLATE utf8_general_ci;