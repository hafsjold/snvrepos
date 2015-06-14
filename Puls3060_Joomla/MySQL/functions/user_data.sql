USE puls3060_dk;

DELIMITER $$


CREATE FUNCTION puls3060_dk.user_data (_name varchar(80),
_username varchar(16),
_adresse varchar(60),
_postnr varchar(12),
_bynavn varchar(30),
_mobil varchar(12),
_memberid varchar(5),
_kon varchar(6),
_fodtaar varchar(16),
_message varchar(256))
RETURNS text charset latin1
SQL SECURITY INVOKER
BEGIN
  DECLARE data text;
  IF (_name IS NULL) THEN
    SET _name = '';
  END IF;
  IF (_username IS NULL) THEN
    SET _username = '';
  END IF;
  IF (_adresse IS NULL) THEN
    SET _adresse = '';
  END IF;
  IF (_postnr IS NULL) THEN
    SET _postnr = '';
  END IF;
  IF (_bynavn IS NULL) THEN
    SET _bynavn = '';
  END IF;
  IF (_mobil IS NULL) THEN
    SET _mobil = '';
  END IF;
  IF (_memberid IS NULL) THEN
    SET _memberid = '';
  END IF;
  IF (_kon IS NULL) THEN
    SET _kon = '';
  END IF;
  IF (_fodtaar IS NULL) THEN
    SET _fodtaar = '';
  END IF;
  IF (_message IS NULL) THEN
    SET _message = '';
  END IF;

  SET data = 'O:8:"stdClass":4:{';
  SET data = CONCAT(data, 's:4:"name";s:', LENGTH(CONVERT(_name USING utf8)), ':"', _name, '";');
  SET data = CONCAT(data, 's:8:"username";s:', LENGTH(CONVERT(_username USING utf8)), ':"', _username, '";');
  SET data = CONCAT(data, 's:6:"fields";a:5:{');
  SET data = CONCAT(data, 's:7:"adresse";s:', LENGTH(CONVERT(_adresse USING utf8)), ':"', _adresse, '";');
  SET data = CONCAT(data, 's:6:"postnr";s:', LENGTH(CONVERT(_postnr USING utf8)), ':"', _postnr, '";');
  SET data = CONCAT(data, 's:6:"bynavn";s:', LENGTH(CONVERT(_bynavn USING utf8)), ':"', _bynavn, '";');
  SET data = CONCAT(data, 's:5:"mobil";s:', LENGTH(CONVERT(_mobil USING utf8)), ':"', _mobil, '";');
  SET data = CONCAT(data, 's:8:"memberid";s:', LENGTH(CONVERT(_memberid USING utf8)), ':"', _memberid, '";');
  SET data = CONCAT(data, '}s:17:"membership_fields";a:3:{');
  SET data = CONCAT(data, 's:3:"kon";a:1:{i:0;s:', LENGTH(CONVERT(_kon USING utf8)), ':"', _kon, '";}');
  SET data = CONCAT(data, 's:7:"fodtaar";a:1:{i:0;s:', LENGTH(CONVERT(_fodtaar USING utf8)), ':"', _fodtaar, '";}');
  SET data = CONCAT(data, 's:7:"message";s:', LENGTH(CONVERT(_message USING utf8)), ':"', _message, '";}}');
  RETURN data;
END
$$

DELIMITER ;