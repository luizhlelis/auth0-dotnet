terraform {
  required_providers {
    auth0 = {
      source  = "alexkappa/auth0"
      version = "0.21.0"
    }
  }
}

provider "auth0" {}

# auth0_branding is not updating the logo
resource "auth0_branding" "auth_server_branding" {
  logo_url = "https://raw.githubusercontent.com/luizhlelis/luizhlelis/master/lelis-octocat.png"
  favicon_url = "https://raw.githubusercontent.com/luizhlelis/luizhlelis/master/lelis-octocat.png"
  colors {
    primary = "#BB86FC"
    page_background = "#121212"
  }
  font {
    url = "https://fonts.googleapis.com/css2?family=Nunito&display=swap"
  }
  universal_login {
    body = file("${path.module}/assets/universal-login.html")
  }
}

# resource "auth0_custom_domain" "auth_server_custom_domain" {
#   domain = "auth.luizlelis.com"
#   type = "auth0_managed_certs"
#   verification_method = "txt"
# }

resource "auth0_client" "auth0_dotnet_client" {
  name = "auth0 dotnet client"
  description = "Just playing a bit with Auth0 🔐, Terraform 🏗️ , and dotnet core 🐦"
  app_type = "regular_web"
  custom_login_page_on = true
  is_first_party = true
  is_token_endpoint_ip_header_trusted = true
  token_endpoint_auth_method = "client_secret_post"
  oidc_conformant = true
  callbacks = [ "https://localhost:5001/v1/auth/response-oidc" ]
  allowed_origins = [ "https://localhost" ]
  grant_types = [ "authorization_code", "refresh_token" ]
  allowed_logout_urls = [ "https://localhost:5001/" ]
  web_origins = [ "https://localhost" ]
  jwt_configuration {
    lifetime_in_seconds = 300
    secret_encoded = true
    alg = "RS256"
    scopes = {
      foo = "bar"
    }
  }
  refresh_token {
    rotation_type = "rotating"
    expiration_type = "expiring"
    leeway = 15
    token_lifetime = 1296000
    infinite_idle_token_lifetime = true
    infinite_token_lifetime      = false
    idle_token_lifetime          = 84600
  }
  client_metadata = {
    foo = "zoo"
  }
}