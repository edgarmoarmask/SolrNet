//to run solr
bin/solr start -p 8983

//to add core
bin/solr create -c malaydocuments

_____________________________________________________________________________________________________________________

//cd to solr_home/server/solr malaydocuments core:

//in solrconfig.conf :
//this is for apache TIKA under  <luceneMatchVersion>9.0</luceneMatchVersion> lucene solrconfig.conf

  <lib dir="${solr.install.dir:../../../..}/modules/extraction/lib" regex=".*\.jar" />
  <lib dir="${solr.install.dir:../../../..}/modules/clustering/lib/" regex=".*\.jar" />
  <lib dir="${solr.install.dir:../../../..}/modules/langid/lib/" regex=".*\.jar" />
  <lib dir="${solr.install.dir:../../../..}/modules/ltr/lib/" regex=".*\.jar" />
  <lib dir="${solr.install.dir:../../../..}/modules/scripting/lib/" regex=".*\.jar" />
 

 //this is to use apacheTika to the bottom of the file solrconfig.conf
  <requestHandler name="/update/extract" 
                  startup="lazy"
                  class="solr.extraction.ExtractingRequestHandler" >
    <lst name="defaults">
      <str name="lowernames">true</str>
      <str name="uprefix">ignored_</str>
      <str name="captureAttr">true</str>
      <str name="fmap.a">links</str>
      <str name="fmap.div">ignored_</str>
    </lst>
  </requestHandler>


//this is for suggest
//adding search component and request handler
    <searchComponent name="suggest" class="solr.SuggestComponent">
      <lst name="suggester">
        <str name="name">mySuggester</str>
        <str name="lookupImpl">AnalyzingInfixLookupFactory</str>
        <str name="dictionaryImpl">DocumentDictionaryFactory</str>
        <str name="field">suggester</str>
        <str name="suggestAnalyzerFieldType">text_general</str>
        <str name="buildOnStartup">false</str>
      </lst>
      <lst name="suggester">
        <str name="name">FreeTextSuggester</str>
        <str name="lookupImpl">FreeTextLookupFactory</str>
        <str name="dictionaryImpl">DocumentDictionaryFactory</str>
        <str name="field">suggester</str>
        <str name="weightField">docContent</str>
        <str name="separator"> </str>
        <str name="ngrams">3</str>
        <str name="suggestFreeTextAnalyzerFieldType">text_general</str>
      </lst>
    </searchComponent>

  <requestHandler name="/suggest" class="solr.SearchHandler"
                  startup="lazy" >
    <lst name="defaults">
      <str name="suggest">true</str>
      <str name="suggest.count">10</str>
    </lst>
    <arr name="components">
      <str>suggest</str>
    </arr>
  </requestHandler>


//in "/select" requestHandler 
//this is for global search

      <str name="df">_text_</str>


      
_________________________________________________________________________________________________________________________




things to add to managed-schema:

//add stemm filter for usign text field
//in our case it is for text_general field in index and query analyzer in <field_type text_general
      <filter name="snowballPorter"/>

//adding suggester field that will store all values to fields section
  <field name="suggester" type="text_general" multiValued="true"/>

//in the end add copyField for global search to the bottom of the file

  <copyField source="*" dest="_text_"/>  
  <copyField source="*" dest="suggester"/>  


//after all modification, remember to restart the solr


__________________________________________________________________________________________________________________________

//in dotnet project

//in Program.cs init the solr instance with proper solr core
builder.Services.AddSolrNet<IndexFields>("http://localhost:8983/solr/NewCore");

//in FileController.cs

//edit these variables:
//_solrInstanceDir is path for solr server
private readonly string _solrInstanceDir = "D:\\dev\\solr\\solr\\server\\solr\\";
//_solrCore is name of created core 
private readonly string _solrCore = "NewCore";
