# auth0-dotnet

Just playing a bit with Auth0 🔐, Terraform 🏗️ , and dotnet core 🐦.

## Terraform

To integrate [auth0](https://auth0.com/) with Terraform, it's required to use a community provider. There are more than one community providers, but the one we're using [here](./src/Terraform/main.tf) is the [alexkappa/auth0](https://registry.terraform.io/providers/alexkappa/auth0/latest/docs) one.

First, you need to store the credentials in environment variables:

```bash
export AUTH0_DOMAIN="<domain>"
export AUTH0_CLIENT_ID="<client-id>"
export AUTH0_CLIENT_SECRET="<client_secret>"
```
