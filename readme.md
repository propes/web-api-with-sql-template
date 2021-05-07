# Web Api With SQL Template

This is a starter template for creating a new web service using asp.net core webapi connecting to SQL Server.

It features:
- An azure pipelines file for CI/CD.
- Set up for swagger documentation.
- An arm template for automatically deploying to Azure App Service using IaC.


## Running the api

You can use the dotnet cli from with the `src` folder, e.g.:
```sh
dotnet run -p web-api-with-sql-template.api
```

Alternatively you can run the api using the convenience bash script:
```sh
sh run.sh
```


## Swagger

You can access the swagger api documentation when running locally at https://localhost:5001/swagger.


## Testing

All automated tests within the solution can be run using the dotnet cli from with the `src` folder, e.g.:
```sh
dotnet test
```


## Deployment

The solution includes the following:
- An azure pipelines file: `azure-pipelines.yml`
- An arm template file: `azuredeploy.json`

To deploy the app you will need to:
- Create a pipeline in azure referencing the remote repo you create for this api.
- Replace the placeholders in `azure-pipelines.yml` with appropriate values for your api. The placeholders are denoted by curly braces, e.g. `{ replaceMe }`.
- Push the changes to the remote repo which will create the infrastructure and initiate the CI/CD pipeline.
