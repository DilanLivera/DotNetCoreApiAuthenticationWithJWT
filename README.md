# DotNetCoreApiAuthenticationWithJWT

Demo application to showcase .Net Core API authentication using JSON Web Token

###How to setup
1. Add a controller.
2. Add post route to authenticate the user.
3. Add ```app.UseAuthentication();``` to Startup.cs file. This needs to be before ```UseAuthorization```. This is to configure authentication middleware.
4. For authentication we need a class to verify the username and password against a db (simple mock object is used in the example) and create a JWT if user is verified.
5. Create an interface called ```IJwtAuthenticationManager``` with a ```Authenticate``` method which takes username and password and returns a string which is the jwt.
6. Create ```JwtAuthenticationManager``` class which derives from the ```IJwtAuthenticationManager```.
7. Add ```Microsoft.AspNetCore.Authentication``` package
8. Inside ```Authenticate``` method in ```JwtAuthenticationManager``` class, 
  a. authenticates the user
  b. if the user credentials not verified return null.
  c. else 
    ```
      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      byte[] tokenKey = Encoding.ASCII.GetBytes(_key);
      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
      {
          Subject = new ClaimsIdentity(new Claim[]
          {
              new Claim(ClaimTypes.Name, username)
          }),
          Expires = DateTime.UtcNow.AddHours(1),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
      };

      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

      return  tokenHandler.WriteToken(token);
    ```
9. In the controller create a private ```JwtAuthenticationManager``` field and initialize it in the constructor.
10. In the controller, under authenticate route, call the ```JwtAuthenticationManager```'s authenticate method using the username and password passed in the post request.
  a. if this method returns null, then user is not a valid user.
  b. if this returns the token, send it back to the caller.
11. In the Startup file under ```ConfigureServices``` method add the ```IJwtAuthenticationManager``` to the service.
12. Configure the JWT pipeline.
  a. Add ```services.AddAuthentication()``` to ```ConfigureServices``` in Startup file.
  b. Provide the default authentication scheme and default challenge scheme as jwt defaults. 
  c. 
    ```
      services
        .AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secirityKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    ```
13. Add ```[Authorize]``` attribute to the controller and ```[AllowAnonymous]``` attribute to authenticate route.

###How to test
1. Send a Post request to ```api/course/authenticate``` with a JSON object ```{ "username" : "user1", "password" : "password1" }``` in the request body. This will send back the JWT.
2. Send a Get request to ```api/course``` with bellow details in the request header.
  a. key - Authorization
  b. value - Bearer [jwt token returned from the previous post request. There is a space between word Bearer and the JWT.]
