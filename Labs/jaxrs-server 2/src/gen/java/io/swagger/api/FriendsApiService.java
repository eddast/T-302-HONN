package io.swagger.api;

import io.swagger.api.*;
import io.swagger.model.*;

import org.glassfish.jersey.media.multipart.FormDataContentDisposition;

import io.swagger.model.Friend;
import io.swagger.model.FriendInput;

import java.util.List;
import io.swagger.api.NotFoundException;

import java.io.InputStream;

import javax.ws.rs.core.Response;
import javax.ws.rs.core.SecurityContext;
import javax.validation.constraints.*;
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public abstract class FriendsApiService {
    public abstract Response addFriend(FriendInput body,SecurityContext securityContext) throws NotFoundException;
    public abstract Response deleteFriendById(Long id,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getFriend(Long id,SecurityContext securityContext) throws NotFoundException;
    public abstract Response getFriends(SecurityContext securityContext) throws NotFoundException;
    public abstract Response updateGameById(Long id,FriendInput body,SecurityContext securityContext) throws NotFoundException;
}
