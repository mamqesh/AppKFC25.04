CREATE TABLE [Group] (
	IDGroup integer primary key NOT NULL,
	Course integer NOT NULL,
	Speciality nvarchar(150) NOT NULL,
 
)
GO
CREATE TABLE [Student] (
	StudentTicket integer primary key NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	Surname nvarchar(50) NOT NULL,
	Patronymic nvarchar(50) NOT NULL,
	[Group] integer NOT NULL,
	[Password] nvarchar(100) NOT NULL,
	Foreign key ([Group]) references [Group](IDGroup)
)
GO
CREATE TABLE [Teacher] (
	PersonnelNumber integer primary key NOT NULL,
	[Name] varchar(50) NOT NULL,
	Surname varchar(50) NOT NULL,
	Patronymic varchar(50) NOT NULL,
	[Password] varchar(100) NOT NULL,
	[Role] integer NOT NULL,

)
GO
CREATE TABLE [Subject] (
	SubjectID integer primary key NOT NULL,
	SubjectName varchar(50) NOT NULL,
)
GO
CREATE TABLE [Question] (
	QuestionsID integer primary key NOT NULL,
	Question text NOT NULL,
	[Subject] integer NOT NULL,
	Teacher integer NOT NULL,
	Foreign key ([Subject]) references [Subject](SubjectID),
	Foreign key (Teacher) references Teacher(PersonnelNumber)
)
GO
CREATE TABLE [OptionText] (
	IDOptionText integer primary key NOT NULL,
	Answer varchar(100) NOT NULL,
	TrueFalse varchar(20) NOT NULL,
)
GO
CREATE TABLE [Option] (
	[Option] integer NOT NULL,
	Question integer NOT NULL,
	Foreign key ([Option]) references OptionText(IDOptionText),
	Foreign key (Question) references Question(QuestionsID),
	Primary key([Option],Question)
)
GO
CREATE TABLE [Lesson] (
	LessonID integer primary key NOT NULL,
	[Date] date NOT NULL,
	Lesson varchar(50) NOT NULL,
	[Group] integer NOT NULL,
	[Subject] integer NOT NULL,
	Teacher integer NOT NULL,
	Foreign key ([Group]) references [Group](IDGroup),
	Foreign key ([Subject]) references [Subject](SubjectID),
	Foreign key (Teacher) references Teacher(PersonnelNumber)
)
GO
--ГРУППА
--СТУДЕНТЫ
--УЧИТЕЛЯ
--ПРЕДМЕТЫ 
--ВОПРОСЫ

INSERT INTO  [Group] (IDGroup,Course,Speciality) VALUES
('163', '1', 'Информационные системы и программирование'),
('263', '2', 'Информационные системы и программирование'),
('363', '3', 'Информационные системы и программирование'),
('463', '4', 'Информационные системы и программирование'),
('1063', '1', 'Экономичексая безопасность'),
('2063', '2', 'Экономичексая безопасность'),
('3063', '3', 'Экономичексая безопасность')
GO

INSERT INTO Student(StudentTicket, [Name], Surname, Patronymic, [Group], [Password]) VALUES
('1019','Анна','Кондрашова','Глебовна','163','group163'),
('1071','Кирилл','Смирнов','Матвеевич','163','group163'),
('1143','Эмма','Егорова','Тимофеевна','263','group263'),
('1222','Кира','Медведева','Романовна','263','group263'),
('1308','Андрей','Федосеев','Дмитриевич','363','group363'),
('1374','Диана','Беспалова','Кирилловна','363','group363'),
('1407','Андрей','Иванов','Тимофеевич','2063','group2063'),
('1418','Евгений','Майоров','Игоревич','2063','group2063'),
('1749','Оксана','Ерёмина','Владимировна','2063','group2063'),
('1878','Максим','Кузнецов','Кириллович','2063','group2063')
GO

INSERT INTO Teacher(PersonnelNumber, [Name], Surname, Patronymic, [Password], [Role]) VALUES
('1163','Евгений','Майоров','Игоревич','qwerty123','Преподаватель'),
('1181','Юрий','Майоров','Игоревич','qwerty123','Преподаватель'),
('1249','Владимир','Майоров','Игоревич','qwerty123','Преподаватель'),
('1282','Данил','Майоров','Игоревич','qwerty123','Преподаватель'),
('1422','Данзан','Майоров','Игоревич','qwerty123','Преподаватель')
GO

INSERT INTO [Subject](SubjectID,SubjectName) VALUES 
('1','Русский язык'),
('2','Литература'),
('2','Физика'),
('3','Математика'),
('4','Химия'),
('5','История')

INSERT INTO [Question] (QuestionsID, Question, [Subject], Teacher) VALUES
('1', 'Назовите одну из причин Смутного времени:', '5', '1163'),
('2', 'По итогам Тридцатилетней войны был заключен?', '5', '1181'),
('3', 'Когда закончилось Смутное время:', '5', '1163'),
('4', 'Освобождение Москвы от польских интервентов удалось благодаря действиям:', '5', '1163')
GO

INSERT INTO OptionText(IDOptionText, Answer, TrueFalse) VALUES
('1', 'Вестфальский мир', 'True'),
('2', 'Ништадтский мир', 'False'),
('3', 'Парижский мир', 'False'),
('4', 'Утрехтская уния', 'False'),
('5', 'Пресечение царской династии Рюриковичей', 'True'),
('6', 'Смерть Б. Годунова', 'False'),
('7', 'Реформы Избранной Рады', 'False'),
('8', 'Введение правила Юрьева дня', 'False'),
('9', '1611 г.', 'False'),
('10', '1613 г.', 'True'),
('11', '1598 г.', 'False'),
('12', '1609 г.', 'False'),
('13', 'Семибоярщины', 'False'),
('14', 'Первого ополчения', 'True'),
('15', 'Второго ополчения', 'False'),
('16', 'Михаила Фёдоровича Романова', 'False')

INSERT INTO [Option]([Option],Question) VALUES 
(1,1),
(2,1),
(3,1),
(4,1),
(5,2),
(6,2),
(7,2),
(8,2),
(9,3),
(10,3),
(11,3),
(12,3),
(13,4),
(14,4),
(15,4),
(16,4)
