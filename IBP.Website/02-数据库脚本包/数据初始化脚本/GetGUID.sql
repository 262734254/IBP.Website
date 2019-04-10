declare @cc int;
declare @tt table(idd varchar(50));

set @cc = 0;
while (@cc < 100)
begin
	insert into @tt select '''' + cast(newid() as varchar(max)) + '''';
	set @cc = @cc + 1;
end

select * from @tt;