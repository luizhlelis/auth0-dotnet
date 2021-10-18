# auth0-dotnet

Just playing a bit with Auth0 🔐, Terraform 🏗️ , and dotnet core 🐦.

## Prerequisites

- [Dotnet core 5.0](https://dotnet.microsoft.com/download/)

- [Auth0 Account](https://auth0.com/)

## Terraform

To integrate [auth0](https://auth0.com/) with Terraform, it's required to use a community provider. There are more than one community providers, but the one we're using [here](./src/Terraform/main.tf) is the [alexkappa/auth0](https://registry.terraform.io/providers/alexkappa/auth0/latest/docs) one. Create a new application `Machine to Machine` on [auth0](https://auth0.com/), follow [this article](https://auth0.com/blog/use-terraform-to-manage-your-auth0-configuration/) to see how to created it.

Before `init`,`plan` and `apply` the terraform files, first you need to store the credentials in environment variables:

```bash
export AUTH0_DOMAIN="<domain>"
export AUTH0_CLIENT_ID="<client-id>"
export AUTH0_CLIENT_SECRET="<client-secret>"
```

> **NOTE:** The `client-id` and `client-secret` above are different from the app credentials, those credentials are gonna be created in the next section.

## Applying the infra to Auth0

```bash
terraform init
````

```bash
terraform plan
```

```bash
terraform apply
```

Open the `terraform.tfstate` file to see the just created credentials. Search for `client_id` and `client_secret` there, or otherwise, get them from the `Auth0` portal.

## Running the app

Update the [appsettings.json](./src/Auth0Dotnet/appsettings.json) file with the credentials created in the last section.

To run the server, type the command below inside [Auth0Dotnet](./src/Auth0Dotnet) folder:

```bash
dotnet run
```

To see the registered api routes, open the swagger spec in the browser:

```bash
https://localhost:5001/swagger/index.html
```

You can also navigate in the app in your browser:

```bash
https://localhost:5001/
```
