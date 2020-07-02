$serveraddress=$args[0]
$user=$args[1]
$pass=$args[2]

write "Creating database..."
sqlcmd -S $serveraddress -U $user -P $pass -i db/quiz_db.Database.sql    
write "Creating questions table..."                          
sqlcmd -S $serveraddress -U $user -P $pass -i db/db_create_questions_table.sql -d QuizAppDb 
write "Creating answers table..."                                 
sqlcmd -S $serveraddress -U $user -P $pass -i db/db_create_answers_table.sql -d QuizAppDb    
write "Populating both tables with data..."     
sqlcmd -S $serveraddress -U $user -P $pass -i db/dbo.question.Table.Data.sql -d QuizAppDb       
sqlcmd -S $serveraddress -U $user -P $pass -i db/dbo.answer.Table.Data.sql -d QuizAppDb  