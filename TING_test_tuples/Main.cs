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

			XElement xe_raw = XElement.Load (@"http://opensearch.addi.dk/2.2/?action=search&query=mad&start=1&stepValue=10&sort=date_descending&outputType=xml&allRelations=False&facetName=facet.type&numberOfTerms=10");

			_hitCount = int.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "hitCount").Value);
			_collectionCount = int.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "collectionCount").Value);
			_more = bool.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "more").Value);
			_time = float.Parse (xe_raw.Element (XN_default + "result").Element (XN_default + "time").Value);

			foreach(XElement xe_facet in xe_raw.Element (XN_default + "result").Element (XN_default + "facetResult").Elements())
			{
				string _facetName = xe_facet.Element(XN_default + "facetName").Value;

				foreach(XElement xe_facetTerm in xe_facet.Elements(XN_default + "facetTerm"))
				{
					int _i1 = 0;
					string _i2 = xe_facetTerm.Element(XN_default + "frequence").Value;
					string _i3 = xe_facetTerm.Element(XN_default + "term").Value;
					string _i4 = null;
					string _i5 = null;
					string _i6 = null;
					string _i7 = _facetName;

					var aTuple = Tuple.Create(_i1,_i2,_i3,_i4,_i5,_i6,_i7);

					_list.Add(aTuple);

				}
			};

			foreach (XElement xe_temp in xe_raw.Element(XN_default + "result").Elements(XN_default + "searchResult").Elements())
			{
				int _resultPosition = int.Parse( xe_temp.Element(XN_default +  "resultPosition").Value);

				string _identifier = xe_temp.Element(XN_default + "object").Element(XN_default + "identifier").Value;
				string _formatsAvailable = xe_temp.Element(XN_default + "object").Element(XN_default + "formatsAvailable").Element(XN_default + "format").Value;

				foreach(XElement xe_temp2 in xe_temp.Element(XN_default + "object").Element(XN_dkabm + "record").Elements())
				{
					int    _Item1 = _resultPosition;              //Item1 : The  _resultPosition value
					string _Item2 = xe_temp2.Name.NamespaceName;  //Item2 : The element namespace
					string _Item3 = xe_temp2.Name.LocalName;      //Item3 : The element localname
					string _Item4 = xe_temp2.Value;               //Item4 : The element value

					string _Item5 = null;                         //Item5 : The attribute value (xsi:type) if any
					if(xe_temp2.HasAttributes)
					{
						_Item5 = xe_temp2.FirstAttribute.Value;
					}

					string _Item6 = null;                         //Item6 : null - reserved
					string _Item7 = null;                         //Item7 : null - reserved

					var aTuple = Tuple.Create(_Item1,_Item2,_Item3,_Item4,_Item5,_Item6,_Item7);

					_list.Add(aTuple);

					//Console.WriteLine(_Item1 + " " + _Item2 + " " + _Item3 + " " + _Item4 + " " + _Item5);
				}
			}
			Console.WriteLine("---- End");
			Console.ReadLine();
		}
	}
}
