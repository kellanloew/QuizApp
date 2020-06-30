USE [quiz_db]
GO

/****** Object:  Table [dbo].[answer]    Script Date: 6/29/2020 6:41:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[answer]') AND type in (N'U'))
DROP TABLE [dbo].[answer]
GO

/****** Object:  Table [dbo].[answer]    Script Date: 6/29/2020 6:41:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[answer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AnswerText] nvarchar(1000) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[WasSelected] [bit] NULL,
 CONSTRAINT [PK_answer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[answer]  WITH CHECK ADD  CONSTRAINT [FK_answer_of_question] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[question] ([id])
GO

ALTER TABLE [dbo].[answer] CHECK CONSTRAINT [FK_answer_of_question]
GO

