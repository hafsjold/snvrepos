USE puls3060_dk;

DELIMITER $$


CREATE PROCEDURE puls3060_dk.spCreateMembership (INOUT return_val int(11))
SQL SECURITY INVOKER
BEGIN
  DECLARE cnt int(11);
  DECLARE done int DEFAULT FALSE;
  DECLARE p_count int;
  DECLARE p_count2 int;
  DECLARE p_count3 int;
  DECLARE p_count4 int;
  DECLARE p_username varchar(150);
  DECLARE p_gender varchar(12);
  DECLARE p_fodtaar varchar(12);
  DECLARE p_transactions_id int;
  DECLARE p_return int;
  DECLARE p_membership_id int DEFAULT 6;

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
  DECLARE p_user_id int;
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
    user_id
  FROM puls3060_dk.tblmedlem
  WHERE Status > 0
  ORDER BY Nr ASC;
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
  SET cnt = 0;

  OPEN cur_1;
repeat_loop:
  REPEAT
    FETCH cur_1 INTO p_Nr, p_Kon, p_FodtDato, p_Navn, p_Kaldenavn, p_Adresse, p_Postnr, p_Bynavn, p_Telefon, p_Email, p_Bank, p_Status, p_user_id;
    IF cnt > 450 THEN
      LEAVE repeat_loop;
    END IF;
    SET cnt = cnt + 1;

    SELECT
      COUNT(*) INTO p_count
    FROM ecpwt_rsmembership_subscribers s
      INNER JOIN ecpwt_users u
        ON s.user_id = u.id
    WHERE s.user_id = p_user_id;
    SELECT
      COUNT(*) INTO p_count2
    FROM ecpwt_users u
    WHERE u.id = p_user_id;
    SELECT
      COUNT(*) INTO p_count3
    FROM puls3060_dk.ecpwt_rsmembership_transactions s
    WHERE s.user_id = p_user_id
    AND s.params = 'membership_id=6';
    IF p_count = 1
      AND p_count2 = 1
      AND p_count3 = 0 THEN
    BEGIN
      ##**********************************************************************************
      SELECT
        username INTO p_username
      FROM ecpwt_users
      WHERE id = p_user_id;
      IF (p_Kon = 'K')
        THEN
        SET p_gender = 'Kvinde';
      ELSE
        SET p_gender = 'Mand';
      END IF;
      SET p_fodtaar = YEAR(p_FodtDato);

      INSERT INTO puls3060_dk.ecpwt_rsmembership_transactions (id
      , user_id
      , user_email
      , user_data
      , type
      , params
      , date
      , ip
      , price
      , coupon
      , currency
      , hash
      , custom
      , gateway
      , status
      , response_log)
        VALUES (NULL -- id - INT(11) NOT NULL
        , p_user_id -- user_id - INT(11) NOT NULL
        , p_Email -- user_email - VARCHAR(255) NOT NULL
        , user_data(p_Navn, p_username, p_Adresse, p_Postnr, p_Bynavn, p_Telefon, p_Nr, p_gender, p_fodtaar, '') -- user_data - TEXT NOT NULL
        , 'new' -- type - VARCHAR(32) NOT NULL
        , CONCAT('membership_id=', p_membership_id) -- params - TEXT NOT NULL
        , NOW() -- date - DATETIME NOT NULL
        , '::1' -- ip - VARCHAR(16) NOT NULL
        , 200.00 -- price - DECIMAL(10, 2) NOT NULL
        , '' -- coupon - VARCHAR(64) NOT NULL
        , 'DKK' -- currency - VARCHAR(4) NOT NULL
        , '' -- hash - VARCHAR(255) NOT NULL
        , '' -- custom - VARCHAR(255) NOT NULL
        , 'PBS' -- gateway - VARCHAR(64) NOT NULL
        , 'completed' -- status - VARCHAR(64) NOT NULL
        , CONCAT(DATE_FORMAT(NOW(), '%W, %e. %M %Y'), ' Overført fra gammelt medlems system') -- response_log - TEXT NOT NULL
        );

      SET p_transactions_id = LAST_INSERT_ID();

      INSERT INTO puls3060_dk.ecpwt_rsmembership_membership_subscribers (id
      , user_id
      , membership_id
      , membership_start
      , membership_end
      , price
      , currency
      , status
      , extras
      , notes
      , from_transaction_id
      , last_transaction_id
      , custom_1
      , custom_2
      , custom_3
      , notified
      , published)
        VALUES (NULL -- id - INT(11) NOT NULL
        , p_user_id -- user_id - INT(11) NOT NULL
        , p_membership_id -- membership_id - INT(11) NOT NULL
        , '2015-01-01 00:00:00' -- membership_start - DATETIME NOT NULL
        , '2016-01-01 00:00:00' -- membership_end - DATETIME NOT NULL
        , 200.00 -- price - DECIMAL(10, 2) NOT NULL
        , 'DKK' -- currency - VARCHAR(4) NOT NULL
        , 0 -- status - TINYINT(4) NOT NULL
        , '' -- extras - VARCHAR(255) NOT NULL
        , '' -- notes - TEXT NOT NULL
        , p_transactions_id -- from_transaction_id - INT(11) NOT NULL
        , p_transactions_id -- last_transaction_id - INT(11) NOT NULL
        , '' -- custom_1 - VARCHAR(255) NOT NULL
        , '' -- custom_2 - VARCHAR(255) NOT NULL
        , '' -- custom_3 - VARCHAR(255) NOT NULL
        , '0000-00-00 00:00:00' -- notified - DATETIME NOT NULL
        , 1 -- published - TINYINT(1) NOT NULL
        );

      SELECT
        COUNT(*) INTO p_count4
      FROM puls3060_dk.ecpwt_user_usergroup_map
      WHERE user_id = p_user_id
      AND group_id = 10;
      IF p_count4 = 0 THEN
        INSERT INTO puls3060_dk.ecpwt_user_usergroup_map (user_id
        , group_id)
          VALUES (p_user_id -- user_id - INT(10) NOT NULL
          , 10 -- group_id - INT(10) NOT NULL
          );
      END IF;
    ##**********************************************************************************
    END;
    END IF;


  UNTIL done END REPEAT;
  CLOSE cur_1;

  SET return_val = cnt;
END
$$

DELIMITER ;