.assembly extern mscrolib
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)
	.ver 4:0:0:0
}
.assembly extern TestCILAssembly
{
	.ver 1:0:0:0
}
.assembly ViewFuncCILAssembly
{
	.hash algorithm 0x00008004
	.ver 1:0:0:0
}
.module ViewFuncCILAssembly.exe

.namespace ViewFuncCILAssembly
{
	.class private auto ansi beforefieldinit Program
	 extends [mscorlib]System.Object
	{
		.method private hidebysig static void
		 Main(string[] args) cil managed
		{
			//Поместить точку входа в сборке .exe
			.entrypoint
			.maxstack 8
			
			.locals init ([0] class [TestCILAssembly]TestCILAssembly.Lesson.Teacher teacher)
			ldstr "Vladimir Kurakin"
			ldc.i4 24
			newobj instance void
			 [TestCILAssembly]TestCILAssembly.Lesson.Teacher::.ctor(string, int32)
			stloc.0
			ldloc.0
			call void [TestCILAssembly]TestCILAssembly.Lesson.Teacher::PrintFullName(class [TestCILAssembly]TestCILAssembly.Lesson.Teacher)
			ret
		}
	}
}