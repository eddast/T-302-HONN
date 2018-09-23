package io.swagger.api.factories;

import io.swagger.api.FriendsApiService;
import io.swagger.api.impl.FriendsApiServiceImpl;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class FriendsApiServiceFactory {
    private final static FriendsApiService service = new FriendsApiServiceImpl();

    public static FriendsApiService getFriendsApi() {
        return service;
    }
}
