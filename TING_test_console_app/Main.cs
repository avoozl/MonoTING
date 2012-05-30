using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

using TING.OpenSearch;

namespace TING_test_console_app
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			OpenSearch os = new OpenSearch ();
			
			os.search ();
			
			Console.WriteLine (os.result.ToString ());
			Console.WriteLine ();
			
			Console.ReadLine ();
		}
	}
}
