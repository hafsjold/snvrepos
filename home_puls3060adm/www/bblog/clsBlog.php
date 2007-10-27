<?php
require('clsPuls3060SQL.php');

/* = = = = = = = = = = = = = = = = = = = = */
class clsBlog {

	protected $_DB;
	protected $_id;
	protected $_personid;

	public function __construct(){
		return ;
	} // ctor

	public function setDB($inDB){
		$this->_DB = $inDB;
		return ;
	} // ctor

	private function __get($property) {
		switch ($property) {
			case "id":
			case "personid":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	private function __set($property, $value) {
		switch ($property) {
			case "id":
			case "personid":
				$_prop = '_' . $property;
				$this->$_prop = $value;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
	}

	public function userauth($user, $pass) {
		if ($this->_DB->prepare("SELECT id AS user_id FROM blog.authors WHERE nickname=? AND password=?")) {
			$data = array($user, $pass);
			if ($this->_DB->execute($data)) {
				if ($this->_DB->next(SQL_ASSOC)) {
					$_SESSION['user_id'] = $this->_DB->record['user_id'];
					return $this->_DB->record['user_id'];
				}
				else {
					return false;
				}
			}
		}
	}


}
// End class clsBlog

class clsPost {
	protected $_DB;
	protected $_postid;
	protected $_title;
	protected $_body;
	protected $_posttime;
	protected $_modifytime;
	protected $_status;
	protected $_modifier;
	protected $_sections;
	protected $_ownerid;
	protected $_hidefromhome;
	protected $_allowcomments;
	protected $_autodisabledate;
	protected $_commentcount;

	public function __construct(){
		$this->_DB = new clsPuls3060SQL();
		return ;
	} // ctor

	public function __destruct() {
	}

	private function __get($property) {
		switch ($property) {
			case "postid":
			case "title":
			case "body":
			case "posttime":
			case "modifytime":
			case "status":
			case "modifier":
			case "sections":
			case "ownerid":
			case "hidefromhome":
			case "allowcomments":
			case "autodisabledate":
			case "commentcount":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			case "rows":
				return $this->_DB->result->numRows();
				break;


			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}
	private function __set($property, $value) {
		switch ($property) {
			case "postid":
			case "title":
			case "body":
			case "posttime":
			case "modifytime":
			case "status":
			case "modifier":
			case "sections":
			case "ownerid":
			case "hidefromhome":
			case "allowcomments":
			case "autodisabledate":
			case "commentcount":
				$_prop = '_' . $property;
				$this->$_prop = $value;
				break;

			default:
				throw new Exception("$property is not a valid property");
				break;
		}
	}


	public function deletePost($postid){
		if(!is_numeric($postid)) return false;
		//$this->modifiednow();

		// delete comments
		$Query="DELETE FROM blog.comments WHERE postid=?";
		$QueryDataArr = array($postid);
		$this->_DB->prepare($Query);
		$this->_DB->execute($QueryDataArr);
		// delete post
		$Query="DELETE FROM blog.posts WHERE postid=?";
		$QueryDataArr = array($postid);
		$this->_DB->prepare($Query);
		$this->_DB->execute($QueryDataArr);

		if($this->_DB->affectedRows() == 1)	return true;
		else return false;
	}

	public function readPost($postid){
		$Query="
			SELECT postid, title, body, posttime, modifytime, status, modifier, sections, 
				   ownerid, hidefromhome, allowcomments, autodisabledate, commentcount 
			FROM blog.posts 
			WHERE postid=?
			";
		$QueryDataArr = array($postid);
		$this->_DB->prepare($Query);
		$this->_DB->execute($QueryDataArr);
		if ($this->_DB->next(SQL_ASSOC)) {
			$this->_postid = $this->_DB->record['postid'];
			$this->_title = $this->_DB->record['title'];
			$this->_body = $this->_DB->record['body'];
			$this->_posttime = $this->_DB->record['posttime'];
			$this->_modifytime = $this->_DB->record['modifytime'];
			$this->_status = $this->_DB->record['status'];
			$this->_modifier = $this->_DB->record['modifier'];
			$this->_sections = $this->_DB->record['sections'];
			$this->_ownerid = $this->_DB->record['ownerid'];
			$this->_hidefromhome = $this->_DB->record['hidefromhome'];
			$this->_allowcomments = $this->_DB->record['allowcomments'];
			$this->_autodisabledate = $this->_DB->record['autodisabledate'];
			$this->_commentcount = $this->_DB->record['commentcount'];
			return true;
		}
		else {
			return false;
		}
	}

	public function addPost(){
		$Query="
			INSERT INTO blog.posts (
			  title, 
			  body, 
			  posttime, 
			  modifytime, 
			  status, 
			  modifier, 
			  sections, 
			  ownerid, 
			  hidefromhome, 
			  allowcomments, 
			  autodisabledate, 
			  commentcount)
			 VALUES(?,?,?,?,?,?,?,?,?,?,?,?)  
			";
		$QueryDataArr = array(
		$this->_title,
		$this->_body,
		$this->_posttime,
		$this->_modifytime,
		$this->_status,
		$this->_modifier,
		$this->_sections,
		$this->_ownerid,
		$this->_hidefromhome,
		$this->_allowcomments,
		$this->_autodisabledate,
		$this->_commentcount,
		);

		$this->_DB->prepare($Query);
		$this->_DB->execute($QueryDataArr);

		if($this->_DB->affectedRows() == 1){
			if ($this->_DB->query("select currval('blog.posts_postid_seq') as insert_id", SQL_INIT, SQL_ASSOC)) {
				$this->_postid = $this->_DB->record['insert_id'];
			}
			return true;
		}
		else {
			return false;
		}
	}

	public function updatePost(){
		$Query="
			UPDATE blog.posts SET
			  title=?, 
			  body=?, 
			  posttime=?, 
			  modifytime=?, 
			  status=?, 
			  modifier=?, 
			  sections=?, 
			  ownerid=?, 
			  hidefromhome=?, 
			  allowcomments=?, 
			  autodisabledate=?, 
			  commentcount=? 
			WHERE postid=?
			";
		$QueryDataArr = array(
		$this->_title,
		$this->_body,
		$this->_posttime,
		$this->_modifytime,
		$this->_status,
		$this->_modifier,
		$this->_sections,
		$this->_ownerid,
		$this->_hidefromhome,
		$this->_allowcomments,
		$this->_autodisabledate,
		$this->_commentcount,
		$this->_postid
		);

		$this->_DB->prepare($Query);
		$this->_DB->execute($QueryDataArr);

		if($this->_DB->affectedRows() == 1)	return true;
		else return false;
	}

	public function initNewPost(){
		$this->_postid = 0;
		$this->_title = '';
		$this->_body = '';
		$this->_posttime = time();
		$this->_modifytime = time();
		$this->_status = 'live';
		$this->_modifier = 'simpl';
		$this->_sections = '';
		$this->_ownerid = 1;
		$this->_hidefromhome = 0;
		$this->_allowcomments = 'allow';
		$this->_autodisabledate = time()+ (90 * 24 * 60 * 60);
		$this->_commentcount = 0;
	}

	public function initPostList(){
		$Query="
			SELECT postid, title, body, posttime, modifytime, status, modifier, sections, 
				   ownerid, hidefromhome, allowcomments, autodisabledate, commentcount 
			FROM blog.posts 
			ORDER BY posttime DESC
			";
		$QueryDataArr = array();
		$this->_DB->prepare($Query);
		$this->_DB->execute($QueryDataArr);
	}

	public function nextPostList(){
		if ($this->_DB->next(SQL_ASSOC)) {
			$this->_postid = $this->_DB->record['postid'];
			$this->_title = $this->_DB->record['title'];
			$this->_body = $this->_DB->record['body'];
			$this->_posttime = $this->_DB->record['posttime'];
			$this->_modifytime = $this->_DB->record['modifytime'];
			$this->_status = $this->_DB->record['status'];
			$this->_modifier = $this->_DB->record['modifier'];
			$this->_sections = $this->_DB->record['sections'];
			$this->_ownerid = $this->_DB->record['ownerid'];
			$this->_hidefromhome = $this->_DB->record['hidefromhome'];
			$this->_allowcomments = $this->_DB->record['allowcomments'];
			$this->_autodisabledate = $this->_DB->record['autodisabledate'];
			$this->_commentcount = $this->_DB->record['commentcount'];
			return true;
		}
		else {
			return false;
		}
	}

}
// End class clsPost

class clsSection {
	protected $_DB;
	protected $_sectionid;
	protected $_nicename;
	protected $_name;

	public function __construct(){
		$this->_DB = new clsPuls3060SQL();
		$this->initSectionList();
		return ;
	} // ctor

	public function __destruct() {
	}

	private function __get($property) {
		switch ($property) {
			case "sectionid":
			case "nicename":
			case "name":
				$_prop = '_' . $property;
				return $this->$_prop;
				break;

			case "rows":
				return $this->_DB->result->numRows();
				break;


			default:
				throw new Exception("$property is not a valid property");
				break;
		}
		return null;
	}

	public function initSectionList(){
		$Query="SELECT sectionid, nicename, name FROM blog.sections ORDER BY name";
		$QueryDataArr = array();
		$this->_DB->prepare($Query);
		$this->_DB->execute($QueryDataArr);
	}

	public function nextSectionList(){
		if ($this->_DB->next(SQL_ASSOC)) {
			$this->_sectionid = $this->_DB->record['sectionid'];
			$this->_nicename = $this->_DB->record['nicename'];
			$this->_name = $this->_DB->record['name'];
			return true;
		}
		else {
			return false;
		}
	}

}
// End class clsSection



//$db = new clsPuls3060SQL();
//$clsBlog = new clsBlog();
//$clsBlog->setDB($db);
//$clsBlog->userauth('admin', 'mha733');

//$objPost = new clsPost();
//$objPost->initPostList();
//while ($objPost->nextPostList()){
//    echo "$objPost->title<br/>";
//}

//$objPost = new clsPost();
//$retur = $objPost->deletePost(4);

//$objPost = new clsPost();
//$retur = $objPost->readPost(28);
//$objPost->body = "zxcvbnm";
//$objPost->addPost();

//$objSection = new clsSection();
//while ($objSection->nextSectionList()){
//    echo $objSection->name;
//}



/* = = = = = = = = = = = = = = = = = = = = */
?>
