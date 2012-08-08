using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;

namespace TING.OpenSearch
{
	public class result_class
	{
		public List<result_class_cell> ac_identifier		{get; set;}
		public List<result_class_cell> ac_source			{get; set;}
		public List<result_class_cell> dc_title				{get; set;}
		public List<result_class_cell> dc_creator   		{get; set;}
		public List<result_class_cell> dc_subject   		{get; set;}
		public List<result_class_cell> dc_description   	{get; set;}
		public List<result_class_cell> dcterms_audience   	{get; set;}
		public List<result_class_cell> dc_publisher   		{get; set;}
		public List<result_class_cell> dc_contributor   	{get; set;}
		public List<result_class_cell> dc_date   			{get; set;}
		public List<result_class_cell> dc_type   			{get; set;}
		public List<result_class_cell> dc_format   			{get; set;}
		public List<result_class_cell> dc_identifier   		{get; set;}
		public List<result_class_cell> dc_source   			{get; set;}
		public List<result_class_cell> dc_language   		{get; set;}
		public List<result_class_cell> dc_relation   		{get; set;}
		public List<result_class_cell> dc_rights   			{get; set;}
		public List<result_class_cell> dc_coverage   		{get; set;}
	}	
	public class result_class_cell
	{
		public string element_content;
		public string xsi_type;
		public string element_name;
	}
	public class facet_class
	{
		public string facetName;
		public List<string> frequenceAndTerm;
	}
	
	
	
	//Profile system for produce simpler classes
	public class ResultProfile
	{
		
	}
	

	public class OpenSearch
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OpenSearch._OpenSearch"/> class.
		/// </summary>
		public OpenSearch ()
		{
		}
		
		//OpenSearch namespaces
		
		// Default namespace
		public XNamespace df = @"http://oss.dbc.dk/ns/opensearch";
		public XNamespace dkdcplus = @"http://biblstandard.dk/abm/namespace/dkdcplus/";
		public XNamespace ac = @"http://biblstandard.dk/ac/namespace/";
		public XNamespace dcterms = @"http://purl.org/dc/terms/";
		public XNamespace dkabm = @"http://biblstandard.dk/abm/namespace/dkabm/";
		public XNamespace dc = @"http://purl.org/dc/elements/1.1/";
		public XNamespace oss = @"http://oss.dbc.dk/ns/osstypes";
		public XNamespace xsi = @"http://www.w3.org/2001/XMLSchema-instance";
		
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
		
		//Return result from main web service call
		public XDocument result = new XDocument();
		
		//Error from xml services
		private string OpenSearch_error;
		
		//Last Exception from Web Service
		Exception lastException = new Exception();
		
		
		public List<result_class> getResult()
		{	
			var temp = from r in result.Descendants(dkabm + "record")
				select new result_class
				{
					ac_identifier 				= get_ac_identifier(r),
					ac_source					= get_ac_source(r),
					dc_title					= get_dc_title(r),
					dc_creator 					= get_dc_creator(r),
					dc_subject					= get_dc_subject(r),
					dc_description				= get_dc_description(r),
					dcterms_audience			= get_dcterms_audience(r),
					dc_publisher				= get_dc_publisher(r),
					dc_contributor				= get_dc_contributor(r),
					dc_date						= get_dc_date(r),
					dc_type						= get_dc_type(r),
					dc_format					= get_dc_format(r),
					dc_identifier				= get_dc_identifier(r),
					dc_source					= get_dc_source(r),
					dc_language					= get_dc_language(r),
					dc_relation					= get_dc_relation(r),
					dc_rights					= get_dc_rights(r),
					dc_coverage					= get_dc_coverage(r)
				};
			return temp.ToList();
		}
		
		public List<facet_class> getFacets()
		{
			var temp = from r in result.Descendants(this.df + "facet")
				select new facet_class
				{
					facetName = r.Element(df + "facetName").Value,
					frequenceAndTerm = getfrequenceAndTerm(r)	
				};
			
			return temp.ToList();
		}
		
		private List<result_class_cell> get_ac_identifier (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(ac + "identifier"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "ac:identifier";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			
			
			
			return toReturn;
		}
		private List<result_class_cell> get_ac_source (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(ac + "source"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "ac:source";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			
			
			
			return toReturn;
		}
		private List<result_class_cell> get_dc_title (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "title"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:title";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			
			foreach(XElement xe in target.Descendants(dcterms + "alternative"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:alternative";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			
			
			return toReturn;	
		}
		private List<result_class_cell> get_dc_creator (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "creator"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:creator";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			return toReturn;	
		}
		private List<result_class_cell> get_dc_subject (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "subject"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:subject";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			return toReturn;	
		}
		private List<result_class_cell> get_dc_description (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "description"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:description";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			foreach(XElement xe in target.Descendants(dcterms + "abstract" ))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:abstract";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			foreach(XElement xe in target.Descendants(dkdcplus + "version"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dkdcplus:version";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			
			return toReturn;	
		}	
		private List<result_class_cell> get_dcterms_audience (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dcterms + "audience"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:audience";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			return toReturn;	
		}		
		private List<result_class_cell> get_dc_publisher (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "publisher"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:publisher";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			return toReturn;	
		}	
		private List<result_class_cell> get_dc_contributor (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "contributor"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:contributor";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			return toReturn;	
		}
		private List<result_class_cell> get_dc_date (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "date"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:date";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			return toReturn;	
		}		
		private List<result_class_cell> get_dc_type (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "type"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:type";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			return toReturn;	
		}	
		private List<result_class_cell> get_dc_format (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "format"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:format";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			foreach(XElement xe in target.Descendants(dcterms + "extent" ))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:extent";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			return toReturn;	
		}
		private List<result_class_cell> get_dc_identifier (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "identifier"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:identifier";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			return toReturn;	
		}
		private List<result_class_cell> get_dc_source (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "source"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:source";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			return toReturn;	
		}	
		private List<result_class_cell> get_dc_language (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "language"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:language";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			return toReturn;	
		}	
		private List<result_class_cell> get_dc_relation (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dcterms + "isPartOf"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:isPartOf";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			foreach(XElement xe in target.Descendants(dcterms + "hasPart"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:hasPart";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			foreach(XElement xe in target.Descendants(dcterms + "isReplacedBy"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:isReplacedBy";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			foreach(XElement xe in target.Descendants(dcterms +  "replaces"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:replaces";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			foreach(XElement xe in target.Descendants(dcterms + "references"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:references";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			return toReturn;	
		}	
		private List<result_class_cell> get_dc_rights (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dc + "rights"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dc:rights";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			return toReturn;	
		}	
		private List<result_class_cell> get_dc_coverage (XElement target)
		{
			result_class_cell temp;
			
			List<result_class_cell> toReturn = new List<result_class_cell> ();
			
			foreach(XElement xe in target.Descendants(dcterms + "spatial"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:spatial";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			foreach(XElement xe in target.Descendants(dcterms + "temporal"))
			{
				temp = new result_class_cell();
				
				temp.element_name = "dcterms:temporal";
				temp.element_content = xe.Value;
				temp.xsi_type = null;
				
				if(xe.HasAttributes)
				{
					foreach(XAttribute xa in xe.Attributes())
						temp.xsi_type = xa.Value;
				}
				
				toReturn.Add(temp);	
			}
			
			
			return toReturn;	
		}
		
		private List<string> getfrequenceAndTerm(XElement target)
		{
			string temp;
			
			List<string> toReturn = new List<string> ();
			
			foreach(XElement xe in target.Descendants(df + "facetTerm"))
			{	
				temp = xe.Element(df + "frequence").Value + ":" + xe.Element(df + "term").Value;
				
				toReturn.Add(temp);	
			}
			return toReturn;
		}
		
		
		public void search (
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
							int _numberOfTerms = 0
						   )
		{
			try {
				
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
				if (_facetNames != "") 
				{
					foreach (string s in _facetNames.Split(':'))
					{
						buildQuery.Append ("&facetName=" + s);	
					}
					
					buildQuery.Append ("&numberOfTerms=" + _numberOfTerms.ToString ());
				};
				
				
				//Get result from web service source
				result = XDocument.Load(buildQuery.ToString());	
				
				
			} 
			catch (Exception ex) 
			{
				lastException = ex;
			}
		}
	}
}

