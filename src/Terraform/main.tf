provider "auth0" {}

resource "auth0_branding" "auth_server_branding" {
    logo_url = "https://raw.githubusercontent.com/luizhlelis/luizhlelis/master/lelis-octocat.png"
    colors {
        primary = "#BB86FC"
        page_background = "#121212"
    }
}

resource "auth0_client" "auth0_dotnet_client" {
  name = "auth0 dotnet client"
  description = "Just playing a bit with Auth0 🔐, Terraform 🏗️ , and dotnet core 🐦"
  app_type = "regular_web"
  custom_login_page_on = true
  is_first_party = true
  is_token_endpoint_ip_header_trusted = true
  token_endpoint_auth_method = "client_secret_post"
  oidc_conformant = false
  callbacks = [ "https://localhost:5001/response-oidc" ]
  allowed_origins = [ "https://localhost" ]
  grant_types = [ "authorization_code", "refresh_token" ]
  allowed_logout_urls = [ "https://localhost" ]
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
    token_lifetime = 84600
    infinite_idle_token_lifetime = true
    infinite_token_lifetime      = false
    idle_token_lifetime          = 1296000
  }
  client_metadata = {
    foo = "zoo"
  }
  addons {
    firebase = {
      client_email = "john.doe@example.com"
      lifetime_in_seconds = 1
      private_key = "wer"
      private_key_id = "qwreerwerwe"
    }
    samlp {
      audience = "https://example.com/saml"
      mappings = {
        email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
      }
      create_upn_claim = false
      passthrough_claims_with_no_mapping = false
      map_unknown_claims_as_is = false
      map_identities = false
      name_identifier_format = "urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"
      name_identifier_probes = [
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
      ]
    }
  }
}