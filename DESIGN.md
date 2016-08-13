#![Logo](https://media.glassdoor.com/sqll/35595/return-path-squarelogo-1424627178627.png)

# Return Path

#### [API Documentation](README.md)

## Design & Implementation Doc

### Concept

Originally, the project specification was relatively easy to accomplish and
there are much simplier API models I could of adopted to complete this challenge quicker.
However, I was looking for both a **challenge** and, an oppurtunity to hopefully dig through a bit of data.
I started with the idea of doing a Word Cloud based graphic but eventually settled on doing a bar graph.

I looked for online dataets containing large amounts of messaging information but I could not find too much. The next best source of
information that seemed easy to strip was Wikipedia. I prepopulated the dataset with sentences from Wikipedia in Oil & Gas companies.
I developed an **automated script** to do this.


**An important note on this project:** Although it did  take me the full three days to complete this, I started the timer in the middle of the work week and only managed to work on this outside my working hours from 7 - 5.

###Software Design&nbsp;&nbsp;![alt interesting](https://cdn0.iconfinder.com/data/icons/octicons/1024/server-16.png)




####Frontend & Backend Framework

On the original list given, I did notice that .Net Core was not explictly listed but, can very easily fall into the **.etc** category.
Node.JS was my first gut instinct at this application but I settled for .NET Core because I have a pretty quick workflow setup with
this environment already.

####Data Storage

I have mostly used SQL, transactional databases but I am really drawn to the simplicity and flexibility of NoSQL, document based data storage.
I wanted to have an oppurtunity to create some sort of **MapReduce** function and decided to use MongoDB.
There were some initial drawbacks due to the .Net Core MongoDB driver not being fully matured and it took some time to find enough information
to properly get that working.

####Deployment

Deployment is containerized using a Docker-Compose file. I had issues with the MongoDb driver and using DNS addresses such as
`localhost` or `mongodb:27017`, due to the beta MongoDb .Net Core driver. The project was deployed to a personal development server.

####Security

Security is a big deal. Microsoft was good support for many different authentication model. Currently, this project has no built in security.
Generally, I use a simple JWT based authentication but I wanted to focus mainly on developing a well written API project and integrating MongoDb.

![Software Development](https://cdn1.iconfinder.com/data/icons/flat-business-icons/128/server-128.png)

@ **Alex Bello**
August 2016
