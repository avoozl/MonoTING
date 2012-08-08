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
			OpenSearchTuples ost = new OpenSearchTuples ();

			string s = ost.buildSearch (_facetNames: "facet.type", _numberOfTerms: 10, _stepValue: 1);


			Console.WriteLine (s);

			ost.search (s);

			foreach (var t in ost._list) 
			{
				Console.WriteLine (t.Item2);
			}

			Console.WriteLine();
			Console.ReadLine();


		}
	}
}
