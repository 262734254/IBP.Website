select '<span ''cid=''' + CAST(id as varchar) + '''  ''id=''' + CAST(province_id as varchar) + '''>' + province_name + '</span>' from china_info 
where province_area_id = 3 and province_id is not null and city_id is null;


select '<span ''cid=''' + CAST(id as varchar) + '''  ''id=''' +  CAST(city_id as varchar) + '''>' +  city_name + '</span>' from china_info 
where province_id = 11 and city_id is not null and county_id is null


select '<span ''cid=''' + CAST(id as varchar) + '''  ''id=''' +  CAST(county_id as varchar) + '''>' +  county_name + '</span>' from china_info 
where city_id = 394 and county_id is not null
		

select '{ ''cid'' : ''' + CAST(id as varchar) + ''', ''id'' : ''' + CAST(province_area_id as varchar) + ''', ''name'' : ''' + province_area_name + '''},' from china_info where province_id is null;

select '{ ''cid'' : ''' + CAST(id as varchar) + ''', ''id'' : ''' + CAST(province_id as varchar) + ''', ''name'' : ''' + province_name + '''},' from china_info 
where province_area_id = 3 and province_id is not null and city_id is null;

declare @provinceid int;
set @provinceid = 32;

while @provinceid < 33
	begin
		select 'City' + CAST(@provinceid as varchar) + ': ['
		union all
		select '{ ''cid'' : ''' + CAST(id as varchar) + ''', ''id'' : ''' + CAST(city_id as varchar) + ''', ''name'' : ''' + city_name + '''},' from china_info 
		where province_id = @provinceid and city_id is not null and county_id is null
		union all
		select '],';
		
		set @provinceid = @provinceid + 1;
	end
	
	
declare @cityid int;
set @cityid = 435;

while @cityid < 438
	begin
		select 'County' + CAST(@cityid as varchar) + ': ['
		union all
		select '{ ''cid'' : ''' + CAST(id as varchar) + ''', ''id'' : ''' + CAST(county_id as varchar) + ''', ''name'' : ''' + county_name + '''},' from china_info 
		where city_id = @cityid and county_id is not null
		union all
		select '],';
		
		set @cityid = @cityid + 1;
	end