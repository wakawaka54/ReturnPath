# Return Path

## API Endpoints

### Sentences

#### GET `/api/sentences/?page=[PAGE]`
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Returns a paginated list of sentences in database.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**Reponse:**

```json
{
  "pagination": {
    "current": 0,
    "total": 2
  },
  "sentences": [
    {
      "id": {
        "timestamp": 1471069731,
        "machine": 4941610,
        "pid": 1,
        "increment": 6944523,
        "creationTime": "2016-08-13T06:28:51Z"
      },
      "sentence": "why is this great",
      "tags": [
        "why",
        "great"
      ]
    }
  ]
}
```

#### POST `/api/sentences`
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Creates a sentence object in the database and assigns cooresponding tags.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**Example Request:**

```json
{
  "sentence":"this is really amazing if you think about it"
}
```

#### DELETE `/api/sentences/{id}`
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deletes a sentence object in the database

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**Example Url:** `/api/sentences/26723478dede714`

#### GET `/api/sentences/statistics`
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Retrieves the top 15 tags and their frequency counts on the entire current dataset. 

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**Example Response:**
```json
[
  {
    "tag": "great",
    "count": 30
  },
  {
    "tag": "why",
    "count": 30
  }
]
```
