package io.swagger.api.factories;

import io.swagger.api.BorrowsApiService;
import io.swagger.api.impl.BorrowsApiServiceImpl;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class BorrowsApiServiceFactory {
    private final static BorrowsApiService service = new BorrowsApiServiceImpl();

    public static BorrowsApiService getBorrowsApi() {
        return service;
    }
}
