USE puls3060_dk;

DELIMITER $$


CREATE DEFINER = 'puls3060'@'%'
FUNCTION puls3060_dk.newUserName (Name varchar(255))
RETURNS varchar(10) charset latin1
BEGIN
  DECLARE w1,
          w2,
          w3,
          w4,
          w,
          w_user_name,
          user_name varchar(255);
  DECLARE cnt,
          i int;
  SET w = Name;
  SET w1 = SUBSTRING_INDEX(w, ' ', 1);
  SET w = TRIM(SUBSTRING(w, LENGTH(SUBSTRING_INDEX(w, ' ', 1)) + 1));
  SET w2 = SUBSTRING_INDEX(w, ' ', 1);
  SET w = TRIM(SUBSTRING(w, LENGTH(SUBSTRING_INDEX(w, ' ', 1)) + 1));
  SET w3 = SUBSTRING_INDEX(w, ' ', 1);
  SET w = TRIM(SUBSTRING(w, LENGTH(SUBSTRING_INDEX(w, ' ', 1)) + 1));
  SET w4 = SUBSTRING_INDEX(w, ' ', 1);
  SET w = TRIM(SUBSTRING(w, LENGTH(SUBSTRING_INDEX(w, ' ', 1)) + 1));

  IF LENGTH(w4) > 0 THEN
    SET w_user_name = LOWER(CONCAT(SUBSTRING(w1, 1, 1), SUBSTRING(w2, 1, 1), SUBSTRING(w3, 1, 1), SUBSTRING(w4, 1, 1)));
  ELSEIF LENGTH(w3) > 0 THEN
    SET w_user_name = LOWER(CONCAT(SUBSTRING(w1, 1, 1), SUBSTRING(w2, 1, 1), SUBSTRING(w3, 1, 2)));
  ELSEIF LENGTH(w2) > 0 THEN
    SET w_user_name = LOWER(CONCAT(SUBSTRING(w1, 1, 2), SUBSTRING(w2, 1, 2)));
  ELSE
    SET w_user_name = LOWER(CONCAT(SUBSTRING(w1, 1, 4)));
  END IF;

  SET i = 0;
  REPEAT
    IF i > 0 THEN
      SET user_name = CONCAT(w_user_name, i);
    ELSE
      SET user_name = w_user_name;
    END IF;
    SELECT
      COUNT(*) INTO cnt
    FROM ecpwt_users
    WHERE username = user_name;
    SET i = i + 1;
  UNTIL cnt = 0 END REPEAT;

  RETURN user_name;
END
$$

DELIMITER ;