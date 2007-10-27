
<table width="500" border="1">
  <tr>
    <th scope="col">Transaktion ID</th>
    <th scope="col">Ordrenr.</th>
    <th scope="col">Beløb</th>
    <th scope="col">Valuta</th>
    <th scope="col">Kort</th>
    <th scope="col">Status</th>
    <th scope="col">________Dato________</th>
  </tr>
{while $trans->nextTransList()}  <tr>
    <td>{$trans->transaction}</td>
    <td>{$trans->ordernum}</td>
    <td>{$trans->amount}</td>
    <td>{$trans->currency}</td>
    <td>{$trans->cardtype}</td>
    <td>{$trans->state}</td>
    <td>{$trans->time}</td>
  </tr>
{/while}
</table>