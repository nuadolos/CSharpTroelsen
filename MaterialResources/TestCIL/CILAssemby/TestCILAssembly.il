.assembly extern mscorlib
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}
.assembly extern System.Windows.Forms
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}
.assembly extern TestLibrary
{
	.publickeytoken = (EF 46 26 8E 3E 61 45 74)
	.ver 1:1:0:0
}
.assembly TestCILAssembly
{
	.hash algorithm 0x00008004
	.ver 1:0:0:0
}
.module TestCILAssembly.dll

.namespace TestCILAssembly.Lesson
{
	//.class public ITeacher { }

	.class public Teacher 
	{ 
		.field private string fullName
		.field private int32 experience

		.method public hidebysig specialname
		 instance string get_FullName() cil managed
		{
			ldarg.0
			ldfld string TestCILAssembly.Lesson.Teacher::fullName
			ret
		}
		.method public hidebysig specialname
		 instance void set_FullName(string 'value') cil managed
		{
			ldarg.0
			ldarg.1
			stfld string TestCILAssembly.Lesson.Teacher::fullName
			ret
		}
		.property instance string FullName()
		{
			.get instance string
			 TestCILAssembly.Lesson.Teacher::get_FullName()
			.set instance void
			 TestCILAssembly.Lesson.Teacher::set_FullName(string)
		}

		.method public hidebysig specialname
		 instance int32 get_Experience() cil managed
		{
			ldarg.0
			ldfld int32 TestCILAssembly.Lesson.Teacher::experience
			ret
		}
		.method public hidebysig specialname
		 instance void set_Experience(int32 'value') cil managed
		{
			ldarg.0
			ldarg.1
			stfld int32 TestCILAssembly.Lesson.Teacher::experience
			ret
		}
		.property instance int32 Experience()
		{
			.get instance int32
			 TestCILAssembly.Lesson.Teacher::get_Experience()
			.set instance void
			 TestCILAssembly.Lesson.Teacher::set_Experience(int32)
		}

		.method public hidebysig specialname rtspecialname
		 instance void .ctor(string fullName, int32 experience) cil managed
		{
			.maxstack 8
			ldarg.0//Загрузка объекта this в стек и конструктора базового класса
			call instance void [mscorlib]System.Object::.ctor()

			ldarg.0
			ldarg.1
			call instance void TestCILAssembly.Lesson.Teacher::set_FullName(string)
			
			//В случае объект fullName являлся полем
			//stfld string TestCILAssembly.Lesson.Teacher::fullName
			ldarg.0
			ldarg.2
			call instance void TestCILAssembly.Lesson.Teacher::set_Experience(int32)
			ret
		}

		//В качестве параметра можно указать ссылочную переменную
		//с помощью ref (int32& test1); out ([out] int32& test2)
		.method public hidebysig static void PrintFullName(class TestCILAssembly.Lesson.Teacher alo) cil managed
		{
			.maxstack 8
			.locals init ([0] string nameTeacher)
			ldarg.0
			ldfld string TestCILAssembly.Lesson.Teacher::fullName
			call valuetype [System.Windows.Forms]System.Windows.Forms.DialogResult
			 [System.Windows.Forms]System.Windows.Forms.MessageBox::Show(string)
			pop
			ret
		}
	}
		//implements TestCILAssembly.Lesson.ITeacher

	.class public MathTeacher
		extends TestCILAssembly.Lesson.Teacher { }

	//Определение структуры через сокращенный синтаксис объявления
	.class public sealed value Student { }

	.class public sealed enum Classroom
	{
		.field public static literal valuetype
		TestCILAssembly.Lesson.Classroom Small = int32(0)
		.field public static literal valuetype
		TestCILAssembly.Lesson.Classroom Medium = int32(1)
		.field public static literal valuetype
		TestCILAssembly.Lesson.Classroom Big = int32(2)
		.field public specialname rtspecialname int32 value__
	}
}