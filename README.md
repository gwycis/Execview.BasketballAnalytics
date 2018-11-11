# Execview.BasketballAnalytics

Notes:
1. Sample report has inconsistant formatting, for example:
	a) indentation sometimes is 7 spaces and sometimes is 8
	b) standard JSON object curly brace starts on a new line, rather than right after opening an array.
```json 
    {
		  "Players": [
			{
			  "Id": 4,
			  "Position": "SG",
			}
		  ]
		}
```

		NOT like this (as this requires to build custom JSON serialiser)
```json
    {
		  "Players": [{
			  "Id": 4,
			  "Position": "SG",
			}
		  ]
		}
```

2. Armstrong, B.J. in the report the last dot is missing => Armstrong, B.J I don't think this is part of the task, it must be a mistake in sample output.
3. Average team height is incorrect in the sample report. Report states its 205.8 cm, while if you manually convert every value to CM and take average of them we would get 200.7 cm, as the solution also calculates. Conversion constants used from Google.

The way I have addressed the issues is:
1. I have loaded the sample JSON / Deserialized / Serialized back to JSON using standard formatting, which allowed me to compare against systems output in integration test
2. Added the missing DOT in the sample report for "Armstrong, B.J." name
3. Fixed the sample report to have correct value of 200.7 cm instead of 205.8 cm
