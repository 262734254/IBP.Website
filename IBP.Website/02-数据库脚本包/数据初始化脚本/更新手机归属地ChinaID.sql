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
update phone_location_info set city = '����' where city = '�ﴨ';
update phone_location_info set city = '����' where city = '����1';
update phone_location_info set city = '����' where city = '����1';
update phone_location_info set city = '����' where city = '����';
update phone_location_info set city = '����' where city = '�������ƶ������ط���';
update phone_location_info set city = '����' where city = '����1';
update phone_location_info set city = '����' where city = '���ȣ�ǭ�ϣ�';
update phone_location_info set city = '����' where city = 'ǭ��(����)';
update phone_location_info set city = '�ӱ߳�����������' where city = '�ӱ�';
update phone_location_info set city = '���������������' where city = '����';
update phone_location_info set city = '���Ļ���������' where city = '����';
update phone_location_info set city = '�º���徰����������' where city = '�º�';
update phone_location_info set city = '���ϲ���������' where city = '����';
update phone_location_info set city = '��ɽ����������' where city = '��ɽ';
update phone_location_info set city = '�������տ¶�����������' where city = '����';
update phone_location_info set city = '��������������������' where city = '����';
update phone_location_info set city = '�����ɹ������������' where city = '����';
update phone_location_info set city = '�˰���' where city = '�˰�' and province = '���ɹ�';
update phone_location_info set city = '��������������' where city = '����';
update phone_location_info set city = '����' where city = '���ݵ���';
update phone_location_info set city = '���' where city = '�������';
update phone_location_info set city = '���' where city = '���������';
update phone_location_info set city = '��Ȫ' where city = '��Ȫ������';
update phone_location_info set city = '��Ȫ' where city = '��Ȫ��������';
update phone_location_info set city = '����' where city = '�ط���';
update phone_location_info set city = '��ɽ' where city = '��ɽ�Ϻ�';
update phone_location_info set city = '�Ϻ�' where city = '��ͨ��';
update phone_location_info set city = '����' where city = '��������';
update phone_location_info set city = 'ǭ�������嶱��������' where city = '���ǭ���ϣ�';
update phone_location_info set city = 'ǭ���ϲ���������������' where city = '���壨ǭ���ϣ�';
update phone_location_info set city = '��ɽ' where city = '��ɽ(ŭ��)';
update phone_location_info set city = '�������������' where city = '�е�';
update phone_location_info set city = '�����鲼', province='���ɹ�' where city = '���������鲼' and province = '���������鲼';
update phone_location_info set city = '��������' where city = '����';
update phone_location_info set city = '�����첼��' where city = '�����鲼';
update phone_location_info set city = 'ǭ�ϲ���������������' where city = 'ǭ��';
update phone_location_info set city = '����' where city = '����/��Դ';
update phone_location_info set city = '�ӱ߳�����������' where city = '�ӱ�(�Ӽ�/����)';
update phone_location_info set city = '����' where city = '����/�ڽ�';
update phone_location_info set city = '����' where city = '����/����';
update phone_location_info set city = '����' where city = '����/����';
update phone_location_info set city = '����' where city = '����/����';
update phone_location_info set city = '����' where city = '��������У԰';
update phone_location_info set city = '����' where city = '����/���';
update phone_location_info set city = '��ɽ' where city = '��ɽ/üɽ';
update phone_location_info set city = '��ɽ����������' where city = '��ɽ(����)';
update phone_location_info set city = '����' where city = '����/���';
update phone_location_info set city = '���β���������' where city = '����(����)';
update phone_location_info set city = '���Ӳ���Ǽ��������' where city = '����(�����/�봨)';
update phone_location_info set city = '���������ɹ�������' where city = '��������(�����)';
update phone_location_info set city = 'ǭ�������嶱��������' where city = 'ǭ����(����)';
update phone_location_info set city = 'ǭ���ϲ���������������' where city = 'ǭ����(����)';
update phone_location_info set city = '�����ɹ������������' where city = '����';

update china_info set city_name = '�˰���' where id = 290;
update china_info set city_name = 'ɽ��' where city_name='ɽ�ϵ�' and province_id = 26;
update china_info set city_name = '����' where city_name='�����' and province_id = 26;
update china_info set city_name = '��������' where city_name='������' and province_id = 18;

update phone_location_info set china_id = 505 where city = '����';
update phone_location_info set china_id = 180 where city = '����';
update phone_location_info set china_id = 112 where city = '����';
update phone_location_info set china_id = 108 where city = '����';
update phone_location_info set china_id = 411 where city = '����';
update phone_location_info set china_id = 843 where city = '����';
update phone_location_info set china_id = 493 where city = '����/����';
update phone_location_info set china_id = 843 where city = '����(����/����/Ǳ��)';
update phone_location_info set china_id = 941 where city = '(����/����/Ǳ��)';
update phone_location_info set china_id = 1058 where city = '����(����)';


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
												-- select * from phone_location_info where city = '�˰���';
												-- select * from china_info where county_name = @cityname;
												-- select * from china_info where county_name like '%����%'
												-- select * from china_info where city_name like '%����%'
												-- update phone_location_info set china_id = 290 where city = '�˰���'
												-- update phone_location_info set china_id = 387, city = '��ɽ' where city = '��ɽ��'
												-- update china_info set city_name = '�˰���' where id = 290; 
												-- city_name = '�˰�' and province_id = 31;
								
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
						--select * from china_info where city_name like '%����%';
						--select * from china_info where county_name like '%����%';
						--select * from phone_location_info where city = '�˰�';
						--update phone_location_info set city ='����' where city = '����';
						--update phone_location_info set city ='�˰���' where city = '�˰�' and province = '���ɹ�';
						--update china_info set city_name = '����' where city_id = 340 and province_id = 26;
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