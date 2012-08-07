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

			List<Tuple<string,string,string,string,string,string,string>> _list = new List<Tuple<string,string,string,string,string,string,string>> ();

			int _hitCount;
			int _collectionCount;
			bool _more;
			float _time;

			Console.WriteLine ("--- Begin");

			XElement xe_raw = XElement.Load (@"http://opensearch.addi.dk/next_2.2/?action=search&query=hansen&start=1&stepValue=1&outputType=xml&profile=test&agency=100200");

			_hitCount = int.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "hitCount").Value);
			_collectionCount = int.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "collectionCount").Value);
			_more = bool.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "more").Value);
			_time = float.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "time").Value);

			Console.WriteLine ("hitCount: " + _hitCount);
			Console.WriteLine ("collectionCount: " + _collectionCount);
			Console.WriteLine ("more: " + _more);
			Console.WriteLine ("time: " + _time.ToString ());

			foreach (XElement xe_temp in xe_raw.Element(XN_default + "result").Element(XN_default + "searchResult").Elements())
			{
				int _resultPosition = int.Parse( xe_temp.Element(XN_default +  "resultPosition").Value);


				foreach(XElement xe_temp2 in xe_temp.Element(XN_default + "object").Element(XN_dkabm + "record").Elements())
					Console.WriteLine(xe_temp2.Name);
			}


			Console.WriteLine("---- End");
			Console.ReadLine();
		}
	}
}
