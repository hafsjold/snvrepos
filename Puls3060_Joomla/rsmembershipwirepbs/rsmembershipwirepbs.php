<?php
/**
* @package RSMembership!
* @copyright (C) 2009-2014 www.rsjoomla.com
* @license GPL, http://www.gnu.org/licenses/gpl-2.0.html
*/

defined('_JEXEC') or die('Restricted access');

jimport('joomla.plugin.plugin');

class plgSystemRSMembershipWirePbs extends JPlugin
{
	public function __construct(&$subject, $config) {
		parent::__construct($subject, $config);

		if ($this->canRun()) {
			require_once JPATH_ADMINISTRATOR.'/components/com_rsmembership/helpers/rsmembership.php';
			$this->_loadLanguage();
			$this->addOurPayments();
		}
	}
	
	protected function canRun() {
		return file_exists(JPATH_ADMINISTRATOR.'/components/com_rsmembership/helpers/rsmembership.php');
	}
	
	protected function addOurPayments() {
		$db 	= JFactory::getDBO();
		$query	= $db->getQuery(true);

		$query->select('*')
			  ->from($db->qn('#__rsmembership_payments'))
			  ->where($db->qn('published').' = '.$db->q('1'))
			  ->order($db->qn('ordering').' ASC');
		$db->setQuery($query);
		$payments = $db->loadObjectList();
		
		foreach ($payments as $payment) {
			RSMembership::addPlugin($this->getTranslation($payment->name), 'rsmembershipwire'.$payment->id);
		}
	}
	
	protected function getTranslation($text) {
		$lang = JFactory::getLanguage();
		$key  = str_replace(' ', '_', $text);
		if ($lang->hasKey($key)) {
			return JText::_($key);
		} else {
			return $text;
		}
	}

	public function onMembershipPayment($plugin, &$data, $extra, $membership, &$transaction, $html) {
		$this->loadLanguage('plg_system_rsmembership', JPATH_ADMINISTRATOR);
		$this->loadLanguage('plg_system_rsmembershipwire', JPATH_ADMINISTRATOR);

		if (preg_match('#rsmembershipwire([0-9]+)#', $plugin, $match)) {
			$id = $match[1];

			// Store the transaction so we can get an ID
			$transaction->store();
		
			$newData = unserialize($transaction->user_data);			
			if (array_key_exists('fiknr', $newData->membership_fields)) {
				$new_fiknr =  $this->newFiknr($transaction->id, $membership->id);
				$newData->membership_fields['fiknr'] = $new_fiknr;					
				$data->membership_fields['fiknr'] = $new_fiknr;					
			}
			$transaction->user_data = serialize($newData);		
		}
	}
	
	protected function newFiknr($transaction_id, $membership_id) {
		try{
			$fiknr1 = $membership_id* 1000000 +$transaction_id;
			$fiknr2 = $this->luhn_checkdigit($fiknr1);
			$fiknr3 = '000000000000000' . $fiknr1 . $fiknr2;
			$fiknr4 = substr($fiknr3, -15);
			//$fiknr5 = '+71< ' . $fiknr4 . '+81131945<';
			return $fiknr4;
		} catch(Exception $e){
			return '000000000000000';
		}
	}
	
	protected function luhn_check($number) {
		// If the total mod 10 equals 0, the number is valid
		return ($this->luhn_process($number) % 10 == 0) ? TRUE : FALSE;
	}
	
	protected function luhn_process($number) {
		// Strip any non-digits (useful for credit card numbers with spaces and hyphens)
		$number=preg_replace('/\D/', '', $number);
	
		// Set the string length and parity
		$number_length=strlen($number);
		$parity=$number_length % 2;
	
		// Loop through each digit and do the maths
		$total=0;
		for ($i=0; $i<$number_length; $i++) {
			$digit=$number[$i];
			// Multiply alternate digits by two
			if ($i % 2 == $parity) {
				$digit*=2;
				// If the sum is two digits, add them together (in effect)
				if ($digit > 9) {
					$digit-=9;
				}
			}
			// Total up the digits
			$total+=$digit;
		}
		return $total;
	}
	
	protected function luhn_checkdigit($number) {
		return 10 - ($this->luhn_process($number . '0') % 20);
	}
	
	protected function _loadLanguage() {
		$this->loadLanguage('plg_system_rsmembershipwire', JPATH_ADMINISTRATOR);
	}
}