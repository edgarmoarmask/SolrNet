using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uploadTest.Shared
{
    public class IndexFields
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("docName")]
        public List<string> DocName { get; set; }

        [SolrField("docContent")]
        public List<string> DocContent { get; set; }
        
        [SolrField("docMetaDataCreation-Date")]
        public List<DateTime> DocCreationDate { get; set; }
        [SolrField("docMetaDataLast-Modified")]
        public List<DateTime> DocModifiedDate { get; set; }

        [SolrField("docMetaDataAuthor")]
        public List<string> DocAuthor { get; set; }

        [SolrField("docMetaDatalanguage")]
        public List<string> DocLanguage { get; set; }

        [SolrField("docMetaDataContent-Type")]
        public List<string> DocDataType { get; set; }

        [SolrField("docMetaDatastream_size")]
        public List<long> DocSize { get; set; }
        
        [SolrField("docMetaDataxmpTPg_NPages")]
        public List<long> DocPages { get; set; }

        [SolrField("path")]
        public string Path { get; set; }
        
        [SolrField("docMetaData")]
        public List<Dictionary<string, string>> DocMetaData { get; set; }

    }
}
