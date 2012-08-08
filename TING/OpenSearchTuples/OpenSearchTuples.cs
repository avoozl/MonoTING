using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TING.OpenSearch
{
	public class OpenSearchTuples
	{


		XNamespace XN_default = @"http://oss.dbc.dk/ns/opensearch";
		XNamespace XN_xsi = @"http://www.w3.org/2001/XMLSchema-instance";
		XNamespace XN_oss = @"http://oss.dbc.dk/ns/osstypes";
		XNamespace XN_dc = @"http://purl.org/dc/elements/1.1/";
		XNamespace XN_dkabm = @"http://biblstandard.dk/abm/namespace/dkabm/";
		XNamespace XN_dcterms = @"http://purl.org/dc/terms/";
		XNamespace XN_ac = @"http://biblstandard.dk/ac/namespace/";
		XNamespace XN_dkdcplus = @"http://biblstandard.dk/abm/namespace/dkdcplus/";

		public List<Tuple<int,string,string,string,string,string,string>> _list = new List<Tuple<int,string,string,string,string,string,string>> ();

		public int _hitCount;
		public int _collectionCount;
		public bool _more;
		public float _time;


		public enum sort_types 
		{
			date_ascending, date_descending, creator_ascending, creator_descending, random 
		}
		
		public enum rank_types
		{
			rank_general, rank_title, rank_creator, none
		}
		
		public enum relationData_types
		{
			type, uri,full,none
		}

		//
		XElement result;

		//
		XElement result_error;

		//
		Exception e;

		//Constructor
		public OpenSearchTuples (){}

		//Methods
		public string buildSearch (
							string _targetLibrary = @"http://opensearch.addi.dk/2.2/", 
		                    string actionType = "search", 
		                    string q = "mad", 
		                    int _start= 1, 
		                    int _stepValue = 10, 
							rank_types _rank = rank_types.none,
		                    sort_types _sort = sort_types.date_descending, 
		                    string _outputType = "xml",
							bool _allRelations = false,
							relationData_types _relationData = relationData_types.none,
							string _facetNames = "",
							int _numberOfTerms = 5)
		{
			//String to hold the REST query
			StringBuilder buildQuery = new StringBuilder ();
				
			//URl for the current Library
			buildQuery.Append (_targetLibrary.ToString ());
				
			//Add "?" for begin command
			buildQuery.Append ("?");
				
			//Add "action="
			buildQuery.Append ("action=");
				
			//Add action type : current "search" and "getObject"
			buildQuery.Append (actionType.ToString ());
				
			//Add query parameter
			buildQuery.Append ("&query=" + q.ToString ());
				
			//Add start parameter
			buildQuery.Append ("&start=" + _start.ToString ());
				
			//Add stepValue parameter
			buildQuery.Append ("&stepValue=" + _stepValue.ToString ());
				
			//Add rank type (if any)
			if (_rank != rank_types.none)
				buildQuery.Append ("&rank=" + _rank.ToString ());
				
			//Add sort parameter
			buildQuery.Append ("&sort=" + _sort.ToString ());
				
			//Add outputType parameter
			buildQuery.Append ("&outputType=" + _outputType.ToString ());
				
			//Add allRelations parameter
			buildQuery.Append ("&allRelations=" + _allRelations);
				
			//Add relationData parameter
			if (_relationData != relationData_types.none)
				buildQuery.Append ("&relationData=" + _relationData.ToString ());
				
			//Add facets parameters
			if (_facetNames != "") {
				foreach (string s in _facetNames.Split(':')) {
					buildQuery.Append ("&facetName=" + s);	
				}
					
				buildQuery.Append ("&numberOfTerms=" + _numberOfTerms.ToString ());
			};

			return buildQuery.ToString();
		}
				
			

		public void search(string theSearch = @"http://opensearch.addi.dk/next_2.2/?action=search&query=hansen&start=1&stepValue=1&outputType=xml&profile=test&agency=100200")
		{

			XElement xe_raw = XElement.Load (theSearch);


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
					string _Item7 = null;                         //Item7 : basetype: null or facet

					var aTuple = Tuple.Create(_Item1,_Item2,_Item3,_Item4,_Item5,_Item6,_Item7);

					_list.Add(aTuple);

				}
			}
		}







	}
}

