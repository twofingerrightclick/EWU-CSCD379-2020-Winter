# Assignment **7**

To create a web page that uses TypeScript to retrieve the list of authors from the Api project and display them in an html page.

- Create an ListAuthors.html page in the view directory that lists the authors. ✔❌
- Use NSwag to generate an secretsanta-client.ts file against the API project. ✔❌
- Create an list-authors.ts file that: ✔❌
  - Invokes the secretsanta-client API to delete all existing authors and then create a hard coded list of 5-10 authors when the page loads. ✔❌
  - Invokes the secretsanta-client API to retrieve a list of authors as part of the page load. ✔❌
- Populate the ListAuthors.html page leveraging list-authors api to retrieve a list of authors. ✔❌
- Set the default start page when the Web project runs to be the ListAuthors.html page. ✔❌

## Extra Credit

- Add build steps to generate the secretsanta-client.ts file from the csproj file. ✔❌
- Update the yaml file to run the TypeScript unit tests and fail the build if any unit test fails. ✔❌
- Include a search text box that only displays authors whose first or last name contains the value in the text box. ✔❌
