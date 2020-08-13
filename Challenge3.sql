begin tran
ALTER TABLE [School].[dbo].[StudentGrade]
	ADD CONSTRAINT uniqueconstraint UNIQUE(CourseID, StudentID);
	
ALTER TABLE [School].[dbo].[StudentGrade]
	ADD CONSTRAINT number_range_check CHECK((Grade >= 0.00 AND Grade <= 4.00) OR Grade IS NULL);

rollback 
commit