package io.swagger.api;

import io.swagger.api.*;
import io.swagger.model.*;

import org.glassfish.jersey.media.multipart.FormDataContentDisposition;

import io.swagger.model.Rating;
import io.swagger.model.RatingInput;

import java.util.List;
import io.swagger.api.NotFoundException;

import java.io.InputStream;

import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;
import javax.validation.constraints.*;
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public abstract class RatingsApiService {
    public abstract Response deleteUserRatingByTapeId(Long userId,Long tapeId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getUserRatingByTapeId(Long userId,Long tapeId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getUserRatings(Long userId,SecurityContext securityContext) throws NotFoundException;
    public abstract Response postUserRatingByTapeId(Long userId,Long tapeId,RatingInput body,SecurityContext securityContext) throws NotFoundException;
    public abstract Response putUserRatingByTapeId(Long userId,Long tapeId,RatingInput body,SecurityContext securityContext) throws NotFoundException;
}
