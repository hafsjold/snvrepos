--Import + Export
delete tempimpexp;
GO
--import add from tempsync2:
insert into tempimpexp (ie, nr, source, source_id, field_id, value, act)
select 'i' as ie, t1.nr, t1.source, t1.source_id, t1.field_id, t1.value, 'add' as act
from tempsync2 t1
	 left join tblsync t2 
		on t1.nr = t2.nr
		and t1.source = t2.source
		and t1.source_id = t2.source_id
		and t1.field_id = t2.field_id
where t2.source is null;
GO
--import del from tempsync2:
insert into tempimpexp (ie, nr, source, source_id, field_id, value, act)
select 'i' as ie, t1.nr, t1.source, t1.source_id, t1.field_id, t1.value, 'del' as act
from tblsync t1
	 left join tempsync2 t2 
		on t1.nr = t2.nr
		and t1.source = t2.source
		and t1.source_id = t2.source_id
		and t1.field_id = t2.field_id
where t2.source is null;
GO
--import upd from tempsync2:
insert into tempimpexp (ie, nr, source, source_id, field_id, value, act)
select 'i' as ie, t1.nr, t1.source, t1.source_id, t1.field_id, t1.value, 'upd' as act
from tempsync2 t1
	 join tblsync t2 
		on t1.nr = t2.nr
		and t1.source = t2.source
		and t1.source_id = t2.source_id
		and t1.field_id = t2.field_id
where t1.value != t2.value;
GO
--export add from tempsync:
insert into tempimpexp (ie, nr, source, source_id, field_id, value, act)
select 'e' as ie, t1.nr, t1.source, t1.source_id, t1.field_id, t1.value, 'add' as act
from tempsync t1
	 left join tblsync t2 
		on t1.nr = t2.nr
		and t1.source = t2.source
		and t1.source_id = t2.source_id
		and t1.field_id = t2.field_id
where t2.source is null;
GO
--export del from tempsync:
insert into tempimpexp (ie, nr, source, source_id, field_id, value, act)
select 'e' as ie, t1.nr, t1.source, t1.source_id, t1.field_id, t1.value, 'del' as act
from tblsync t1
	 left join tempsync t2 
		on t1.nr = t2.nr
		and t1.source = t2.source
		and t1.source_id = t2.source_id
		and t1.field_id = t2.field_id
where t2.source is null;
GO
--export upd from tempsync:
insert into tempimpexp (ie, nr, source, source_id, field_id, value, act)
select 'e' as ie, t1.nr, t1.source, t1.source_id, t1.field_id, t2.value as newvalue, 'upd' as act
from tblsync t1
	 join tempsync t2 
		on t1.nr = t2.nr
		and t1.source = t2.source
		and t1.source_id = t2.source_id
		and t1.field_id = t2.field_id
where t1.value != t2.value;
GO