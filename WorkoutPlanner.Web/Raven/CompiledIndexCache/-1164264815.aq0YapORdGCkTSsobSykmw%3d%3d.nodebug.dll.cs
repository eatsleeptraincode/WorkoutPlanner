using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;


public class Index_Auto_2fWorkouts_2fByAthleteId : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fWorkouts_2fByAthleteId()
	{
		this.ViewText = @"from doc in docs.Workouts
select new { AthleteId = doc.AthleteId }";
		this.ForEntityNames.Add("Workouts");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Workouts", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				AthleteId = doc.AthleteId,
				__document_id = doc.__document_id
			});
		this.AddField("AthleteId");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("AthleteId");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("AthleteId");
		this.AddQueryParameterForReduce("__document_id");
	}
}
