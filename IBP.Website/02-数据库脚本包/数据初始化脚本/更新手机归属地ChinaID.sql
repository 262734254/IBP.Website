declare @cityname varchar(50);
declare @provincename varchar(50);
declare @counter int;
declare @counter2 int;
declare @counter3 int;
declare @chinaid int;
declare @cc int;
declare @tt int;

set @tt = 0;
set @cc = 500;
update phone_location_info set city = '达州' where city = '达川';
update phone_location_info set city = '扬州' where city = '扬州1';
update phone_location_info set city = '苏州' where city = '苏州1';
update phone_location_info set city = '随州' where city = '随枣';
update phone_location_info set city = '北京' where city = '易索得移动短信特服号';
update phone_location_info set city = '淮阴' where city = '淮阴1';
update phone_location_info set city = '都匀' where city = '都匀（黔南）';
update phone_location_info set city = '都匀' where city = '黔南(都匀)';
update phone_location_info set city = '延边朝鲜族自治州' where city = '延边';
update phone_location_info set city = '伊犁哈萨克自治州' where city = '伊犁';
update phone_location_info set city = '临夏回族自治州' where city = '临夏';
update phone_location_info set city = '德宏傣族景颇族自治州' where city = '德宏';
update phone_location_info set city = '黄南藏族自治州' where city = '黄南';
update phone_location_info set city = '凉山彝族自治州' where city = '凉山';
update phone_location_info set city = '克孜勒苏柯尔克孜自治州' where city = '克州';
update phone_location_info set city = '湘西土家族苗族自治州' where city = '湘西';
update phone_location_info set city = '海西蒙古族藏族自治州' where city = '海西';
update phone_location_info set city = '兴安盟' where city = '兴安' and province = '内蒙古';
update phone_location_info set city = '海北藏族自治州' where city = '海北';
update phone_location_info set city = '贺州' where city = '贺州地区';
update phone_location_info set city = '金昌' where city = '金昌武威';
update phone_location_info set city = '金昌' where city = '金昌／武威';
update phone_location_info set city = '酒泉' where city = '酒泉嘉峪关';
update phone_location_info set city = '酒泉' where city = '酒泉／嘉峪关';
update phone_location_info set city = '北京' where city = '特服号';
update phone_location_info set city = '佛山' where city = '佛山南海';
update phone_location_info set city = '上海' where city = '易通卡';
update phone_location_info set city = '海东' where city = '海东地区';
update phone_location_info set city = '黔东南苗族侗族自治州' where city = '凯里（黔东南）';
update phone_location_info set city = '黔西南布依族苗族自治州' where city = '兴义（黔西南）';
update phone_location_info set city = '保山' where city = '保山(怒江)';
update phone_location_info set city = '迪庆藏族自治州' where city = '中甸';
update phone_location_info set city = '乌兰查布', province='内蒙古' where city = '内蒙乌兰查布' and province = '内蒙乌兰查布';
update phone_location_info set city = '阿拉善盟' where city = '阿盟';
update phone_location_info set city = '乌兰察布盟' where city = '乌兰查布';
update phone_location_info set city = '黔南布依族苗族自治州' where city = '黔南';
update phone_location_info set city = '焦作' where city = '焦作/济源';
update phone_location_info set city = '延边朝鲜族自治州' where city = '延边(延吉/珲春)';
update phone_location_info set city = '资阳' where city = '资阳/内江';
update phone_location_info set city = '海口' where city = '海口/三亚';
update phone_location_info set city = '南宁' where city = '南宁/崇左';
update phone_location_info set city = '梧州' where city = '梧州/贺州';
update phone_location_info set city = '北京' where city = '电信天翼校园';
update phone_location_info set city = '玉林' where city = '玉林/贵港';
update phone_location_info set city = '乐山' where city = '乐山/眉山';
update phone_location_info set city = '凉山彝族自治州' where city = '凉山(西昌)';
update phone_location_info set city = '武威' where city = '武威/金昌';
update phone_location_info set city = '甘孜藏族自治州' where city = '甘孜(康定)';
update phone_location_info set city = '阿坝藏族羌族自治州' where city = '阿坝(马尔康/汶川)';
update phone_location_info set city = '巴音郭楞蒙古自治州' where city = '巴音郭楞(库尔勒)';
update phone_location_info set city = '黔东南苗族侗族自治州' where city = '黔东南(凯里)';
update phone_location_info set city = '黔西南布依族苗族自治州' where city = '黔西南(兴义)';
update phone_location_info set city = '海西蒙古族藏族自治州' where city = '西海';

update china_info set city_name = '兴安盟' where id = 290;
update china_info set city_name = '山南' where city_name='山南地' and province_id = 26;
update china_info set city_name = '阿里' where city_name='阿里地' and province_id = 26;
update china_info set city_name = '阿拉善盟' where city_name='阿拉善' and province_id = 18;

update phone_location_info set china_id = 505 where city = '崇左';
update phone_location_info set china_id = 180 where city = '毫州';
update phone_location_info set china_id = 112 where city = '株洲';
update phone_location_info set china_id = 108 where city = '随州';
update phone_location_info set china_id = 411 where city = '六库';
update phone_location_info set china_id = 843 where city = '江汉';
update phone_location_info set china_id = 493 where city = '柳州/来宾';
update phone_location_info set china_id = 843 where city = '江汉(天门/仙桃/潜江)';
update phone_location_info set china_id = 941 where city = '(天门/仙桃/潜江)';
update phone_location_info set china_id = 1058 where city = '湘西(吉首)';


while(@cc > 1)
	begin
		select top 1 @cityname = city,@provincename = province from phone_location_info where china_id is null group by city,province;
		select @counter = count(1) from china_info where city_name = @cityname and county_id is null;
	
		
		if @counter = 0 
			begin
				select @counter2 = count(1) from china_info where county_name = @cityname;
				
				if @counter2 = 0  
					begin						
						select @counter3 = count(1) from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -1)  + '%' and county_id is null;
						
						if @counter3 = 0
							begin
								select @cityname,'@counter3 = 0';
								select @counter3 = count(1) from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -2)  + '%' and county_id is null;
								if @counter3 = 1 
									begin
										select @chinaid = id from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -2)  + '%' and county_id is null;
										update phone_location_info set china_id = @chinaid where city = @cityname;
									end
								else
									begin
										update phone_location_info set china_id = -1 where city = @cityname;
									
										select * from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -1)  + '%' and county_id is null;
										set @cc = 0;
									end
							end
						
						if @counter3 = 1
							begin
								select @chinaid = id from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -1)  + '%' and county_id is null;
								update phone_location_info set china_id = @chinaid where city = @cityname;
							end
						
						if @counter3 > 1
							begin
								select @counter3 = count(1) from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -2)  + '%' and county_id is null;
								if @counter3 = 1
									begin
										select @chinaid = id from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -2)  + '%' and county_id is null;
										update phone_location_info set china_id = @chinaid where city = @cityname;
									end
								else
									begin
										if len(@cityname) -3 <= 0 
											begin
												select 'len(@cityname) -3) <=0', @cityname;
												set @cc = 0;
											end 
											
										select @counter3 = count(1) from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -3)  + '%' and county_id is null;
										if @counter3 = 1
											begin
												select @chinaid = id from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -3)  + '%' and county_id is null;
												update phone_location_info set china_id = @chinaid where city = @cityname;
											end
										else
											begin
												select @provincename, @cityname, SUBSTRING(@cityname, 1, len(@cityname) - 1), @counter3;
												-- select * from phone_location_info where city = '兴安盟';
												-- select * from china_info where county_name = @cityname;
												-- select * from china_info where county_name like '%海北%'
												-- select * from china_info where city_name like '%海北%'
												-- update phone_location_info set china_id = 290 where city = '兴安盟'
												-- update phone_location_info set china_id = 387, city = '凉山' where city = '凉山州'
												-- update china_info set city_name = '兴安盟' where id = 290; 
												-- city_name = '兴安' and province_id = 31;
								
												set @cc = 0;
											end
									end
								
							end
					end
					
				if @counter2 = 1 
					begin
						select @chinaid = id from china_info where county_name = @cityname;		
						update phone_location_info set china_id = @chinaid where city = @cityname and province = @provincename;
					end
				
				if @counter2 > 1
					begin
						select '@counter2 > 1', @counter2,@provincename, @cityname;
						--select * from china_info where city_name like '%' + SUBSTRING(@cityname, 1, len(@cityname) -1)  + '%' and county_id is null;
						--select * from china_info where city_name like '%柳州%';
						--select * from china_info where county_name like '%克州%';
						--select * from phone_location_info where city = '兴安';
						--update phone_location_info set city ='郴州' where city = '郴化';
						--update phone_location_info set city ='兴安盟' where city = '兴安' and province = '内蒙古';
						--update china_info set city_name = '阿里' where city_id = 340 and province_id = 26;
					end
			end
			
		if @counter = 1 
			begin		
				select @chinaid = id from china_info where city_name = @cityname and county_id is null;		
				update phone_location_info set china_id = @chinaid where city = @cityname;
			end
			
		if @counter > 1 
			begin
				select @counter2 = COUNT(1) from china_info where city_name= @cityname and province_name = @provincename;
				if @counter2 = 0
					begin
						select '@counter2 = 0';
						set @cc = 0;
					end
				
				if @counter2 = 1
					begin
						select @chinaid = id from china_info where city_name= @cityname and province_name = @provincename;
						update phone_location_info set china_id = @chinaid where city = @cityname and province = @provincename;
					end
				
				if @counter2 > 1
					begin
						select @counter3 = count(1) from china_info where city_name= @cityname and province_name = @provincename and county_id is null;
						if @counter3 = 1
							begin
								select @chinaid = id from china_info where city_name= @cityname and province_name = @provincename and county_id is null;
								update phone_location_info set china_id = @chinaid where city = @cityname and province = @provincename;
							end	
						
						if @counter3 <> 1
							begin
								select 'counter2 != 1', * from china_info where city_name= @cityname and province_name = @provincename;					
								set @cc = 0;
							end						
					end
			end				
			
		set @cc = @cc - 1;
	end

select COUNT(1) from (select city,province from phone_location_info where china_id is null group by city,province) a;