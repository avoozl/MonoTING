using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LINQAndTuplesTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			XNamespace XN_default = @"http://oss.dbc.dk/ns/opensearch";
			XNamespace XN_xsi = @"http://www.w3.org/2001/XMLSchema-instance";
			XNamespace XN_oss = @"http://oss.dbc.dk/ns/osstypes";
			XNamespace XN_dc = @"http://purl.org/dc/elements/1.1/";
			XNamespace XN_dkabm = @"http://biblstandard.dk/abm/namespace/dkabm/";
			XNamespace XN_dcterms = @"http://purl.org/dc/terms/";
			XNamespace XN_ac = @"http://biblstandard.dk/ac/namespace/";
			XNamespace XN_dkdcplus = @"http://biblstandard.dk/abm/namespace/dkdcplus/";

			List<Tuple<int,string,string,string,string,string,string>> _list = new List<Tuple<int,string,string,string,string,string,string>> ();

			int _hitCount;
			int _collectionCount;
			bool _more;
			float _time;

			Console.WriteLine ("--- Begin");

			XElement xe_raw = XElement.Load (@"http://opensearch.addi.dk/next_2.2/?action=search&query=hansen&start=1&stepValue=50&outputType=xml&profile=test&agency=100200");

			_hitCount = int.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "hitCount").Value);
			_collectionCount = int.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "collectionCount").Value);
			_more = bool.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "more").Value);
			_time = float.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "time").Value);
			//Console.WriteLine ("hitCount: " + _hitCount);
			//Console.WriteLine ("collectionCount: " + _collectionCount);
			//Console.WriteLine ("more: " + _more);
			//Console.WriteLine ("time: " + _time.ToString ());



			foreach (XElement xe_temp in xe_raw.Element(XN_default + "result").Elements(XN_default + "searchResult").Elements())
			{
				Console.WriteLine("---- Begin collection elements");

				int _resultPosition = int.Parse( xe_temp.Element(XN_default +  "resultPosition").Value);
				//Console.WriteLine("resultPosition:" + _resultPosition);

				string _identifier = xe_temp.Element(XN_default + "object").Element(XN_default + "identifier").Value;
				string _formatsAvailable = xe_temp.Element(XN_default + "object").Element(XN_default + "formatsAvailable").Element(XN_default + "format").Value;

				//Console.WriteLine("identifier:" + _identifier);
				//Console.WriteLine("formatsAvailable:" + _formatsAvailable);

				Console.WriteLine("---- Tuples begin");

				foreach(XElement xe_temp2 in xe_temp.Element(XN_default + "object").Element(XN_dkabm + "record").Elements())
				{
					//Item1 : The  _resultPosition value
					//Item2 : The element namespace
					//Item3 : The element localname
					//Item4 : The element value
					//Item5 : The attribute value (xsi:type) if any
					//Item6 : null - reserved
					//Item7 : null - reserved


					//Console.WriteLine("dkabm record element begin");


					int    _Item1 = _resultPosition;
					Console.WriteLine("Item1: " + _Item1);

					string _Item2 = xe_temp2.Name.NamespaceName;
					Console.WriteLine("Item2:" + _Item2);

					string _Item3 = xe_temp2.Name.LocalName;
					Console.WriteLine("Item3:" + _Item3);

					string _Item4 = xe_temp2.Value;
					Console.WriteLine("Item4:" + _Item4);

					string _Item5 = null;
					if(xe_temp2.HasAttributes)
					{
						_Item5 = xe_temp2.FirstAttribute.Value;
					}
					Console.WriteLine("Item5:" + _Item5);

					string _Item6 = null;
					string _Item7 = null;


					//Console.WriteLine("dkabm record element end");



					//var tuple7 = Tuple.Create("Jane", 90, 87, 93, 67, 100, 92);

				}
				

				Console.WriteLine("---- Tuples end");



			}


			Console.WriteLine("---- End");
			Console.ReadLine();
		}
	}
}
