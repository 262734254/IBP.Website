truncate table [ibp_db].[dbo].[bankcard_type_info];


INSERT INTO [ibp_db].[dbo].[bankcard_type_info]
           ([bankcard_type_id]
           ,[bank_enum_value]
           ,[card_bin_code]
           ,[card_type]
           ,[card_nature]
           ,[card_brand]
           ,[currency_type]
           ,[card_level]
           ,[bankcard_enum_value]
           ,[created_on]
           ,[created_by]
           ,[modified_on]
           ,[modified_by]
           ,[status_code])
     SELECT 
           NEWID(),
           case yh
				when 'C00001' then '9344972F-6CFA-45C6-BE8D-DCE22E96D42B' -- 工行
				when 'C00002' then '46483077-55BD-4493-95FD-F7FF172884C8' -- 农行
				when 'C00003' then '9B899374-0D8E-4930-8C9D-4C217937A8A8' -- 建行
				when 'C00004' then '568A8424-7486-421E-BE23-1922EF632DD9' -- 交行
		   end
           ,[BinNum]
           ,[KType]
           ,[KState]
           ,[KPP]
           ,[BZ]
           ,[KJB],
           case  [KGN] 
			when 0 then 'FEB39D81-26EC-4A20-97F2-F148FDC87AFD' 
			when 1 then '3A79E7D2-81E3-4D8B-BBB3-E1FD07E5E717'
			when 2 then '07A65711-5D33-4CC4-994D-BC266BB5ACC7'
			when 3 then 'D3732C88-7C6C-4A5E-872D-710CAB71C003'
           end
           ,GETDATE()
           ,'admin'
           ,null
           ,null
           ,0
      from Test_TXMS_20111129.dbo.BD_CardInfo 
      where Test_TXMS_20111129.dbo.BD_CardInfo.YH is not null
GO

-- select * from [ibp_db].[dbo].[bankcard_type_info]

-- select b.bank_enum_value, v.value_id from [bankcard_type_info] b left join custom_data_value v on b.bankcard_enum_value = v.value_id
