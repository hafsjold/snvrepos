USE puls3060_dk;

DELIMITER $$


CREATE PROCEDURE puls3060_dk.spCreateProfile (INOUT return_val int(11))
SQL SECURITY INVOKER
BEGIN
  DECLARE cnt int(11);
  DECLARE done int DEFAULT FALSE;
  DECLARE p_count int;
  DECLARE p_count2 int;
  DECLARE p_user_id int;
  DECLARE p_gender varchar(1);
  DECLARE p_return int;

  DECLARE p_Nr int(11);
  DECLARE p_Kon varchar(1);
  DECLARE p_FodtDato datetime;
  DECLARE p_Navn varchar(35);
  DECLARE p_Kaldenavn varchar(25);
  DECLARE p_Adresse varchar(70);
  DECLARE p_Postnr varchar(4);
  DECLARE p_Bynavn varchar(25);
  DECLARE p_Telefon varchar(8);
  DECLARE p_Email varchar(50);
  DECLARE p_Bank varchar(15);
  DECLARE p_Status int(11);
  DECLARE p_a_Adresse varchar(255);
  DECLARE p_a_Bynavn varchar(255);
  DECLARE cur_1 CURSOR FOR
  SELECT
    Nr,
    Kon,
    FodtDato,
    Navn,
    Kaldenavn,
    Adresse,
    Postnr,
    Bynavn,
    Telefon,
    Email,
    Bank,
    Status,
    a_Adresse,
    a_Bynavn
  FROM puls3060_dk.tblmedlem
  WHERE Status > 0
  ORDER BY Nr ASC;
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
  SET cnt = 0;

  OPEN cur_1;
repeat_loop:
  REPEAT
    FETCH cur_1 INTO p_Nr, p_Kon, p_FodtDato, p_Navn, p_Kaldenavn, p_Adresse, p_Postnr, p_Bynavn, p_Telefon, p_Email, p_Bank, p_Status, p_a_Adresse, p_a_Bynavn;
    IF cnt > 500 THEN
      LEAVE repeat_loop;
    END IF;
    SET cnt = cnt + 1;

    SELECT
      COUNT(*) INTO p_count
    FROM ecpwt_rsmembership_subscribers s
      INNER JOIN ecpwt_users u
        ON s.user_id = u.id
    WHERE s.f14 = p_Nr;
    SELECT
      COUNT(*) INTO p_count2
    FROM ecpwt_users u
    WHERE u.email = p_Email;
    IF p_count = 0
      AND p_count2 = 0 THEN
    BEGIN
      ##**********************************************************************************
      INSERT INTO ecpwt_users (id
      , name
      , username
      , email
      , password
      , block
      , sendEmail
      , registerDate
      , lastvisitDate
      , activation
      , params
      , lastResetTime
      , resetCount
      , otpKey
      , otep
      , requireReset)
        VALUES (NULL -- id - INT(11) NOT NULL
        , TRIM(p_Navn) -- name - VARCHAR(255) NOT NULL
        , newUserName(p_Navn) -- username - VARCHAR(150) NOT NULL
        , TRIM(p_Email) -- email - VARCHAR(100) NOT NULL
        , MD5(SUBSTRING(MD5(RAND()) FROM 1 FOR 16)) -- password - VARCHAR(100) NOT NULL
        , 0 -- block - TINYINT(4) NOT NULL
        , 0 -- sendEmail - TINYINT(4)
        , UTC_TIMESTAMP() -- registerDate - DATETIME NOT NULL
        , '0000-00-00 00:00:00' -- lastvisitDate - DATETIME NOT NULL
        , '' -- activation - VARCHAR(100) NOT NULL
        , '' -- params - TEXT NOT NULL
        , '0000-00-00 00:00:00' -- lastResetTime - DATETIME NOT NULL
        , 0 -- resetCount - INT(11) NOT NULL
        , '' -- otpKey - VARCHAR(1000) NOT NULL
        , '' -- otep - VARCHAR(1000) NOT NULL
        , 0 -- requireReset - TINYINT(4) NOT NULL
        );

      SET p_user_id = LAST_INSERT_ID();

      INSERT INTO ecpwt_user_usergroup_map (user_id
      , group_id)
        VALUES (p_user_id -- user_id - INT(10) NOT NULL
        , 14 -- ResetPassword group_id - INT(10) NOT NULL
        );

      INSERT INTO ecpwt_rsmembership_subscribers (user_id
      , f1
      , f2
      , f4
      , f6
      , f14)
        VALUES (p_user_id -- user_id - INT(11) NOT NULL
        , TRIM(p_Adresse) -- f1 - VARCHAR(255) NOT NULL
        , TRIM(p_Bynavn) -- f2 - VARCHAR(255) NOT NULL
        , TRIM(p_Postnr) -- f4 - VARCHAR(255) NOT NULL
        , TRIM(p_Telefon) -- f6 - VARCHAR(255) NOT NULL
        , TRIM(p_Nr) -- f14 - VARCHAR(255) NOT NULL
        );

    ##**********************************************************************************
    END;
    END IF;


  UNTIL done END REPEAT;
  CLOSE cur_1;

  SET return_val = cnt;
END
$$

DELIMITER ;