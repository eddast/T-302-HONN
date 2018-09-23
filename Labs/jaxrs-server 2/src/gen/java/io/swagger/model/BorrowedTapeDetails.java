/*
 * Videotapes Galore API
 * Simple user and video tape storing and management system. Has functionality to 'borrow' video tapes to users via relations between the two tapes. Has additional functionality of managing and storing user rating of video tapes and user recommendations of video tapes. API designed by Edda Steinunn Rúnarsdóttir for the assigment Project #1 in the course T-302-HÖNN for Reykjavik University.
 *
 * OpenAPI spec version: 1.0.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */


package io.swagger.model;

import java.util.Objects;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonCreator;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import io.swagger.model.Tape;
import java.util.Date;
import javax.validation.constraints.*;

/**
 * BorrowedTapeDetails
 */
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaJerseyServerCodegen", date = "2018-09-16T17:01:38.449Z")
public class BorrowedTapeDetails   {
  @JsonProperty("tape")
  private Tape tape = null;

  @JsonProperty("borrowerId")
  private Long borrowerId = null;

  @JsonProperty("borrowerName")
  private String borrowerName = null;

  @JsonProperty("borrowDate")
  private Date borrowDate = null;

  public BorrowedTapeDetails tape(Tape tape) {
    this.tape = tape;
    return this;
  }

  /**
   * Get tape
   * @return tape
   **/
  @JsonProperty("tape")
  @ApiModelProperty(value = "")
  public Tape getTape() {
    return tape;
  }

  public void setTape(Tape tape) {
    this.tape = tape;
  }

  public BorrowedTapeDetails borrowerId(Long borrowerId) {
    this.borrowerId = borrowerId;
    return this;
  }

  /**
   * Get borrowerId
   * @return borrowerId
   **/
  @JsonProperty("borrowerId")
  @ApiModelProperty(value = "")
  public Long getBorrowerId() {
    return borrowerId;
  }

  public void setBorrowerId(Long borrowerId) {
    this.borrowerId = borrowerId;
  }

  public BorrowedTapeDetails borrowerName(String borrowerName) {
    this.borrowerName = borrowerName;
    return this;
  }

  /**
   * Get borrowerName
   * @return borrowerName
   **/
  @JsonProperty("borrowerName")
  @ApiModelProperty(value = "")
  public String getBorrowerName() {
    return borrowerName;
  }

  public void setBorrowerName(String borrowerName) {
    this.borrowerName = borrowerName;
  }

  public BorrowedTapeDetails borrowDate(Date borrowDate) {
    this.borrowDate = borrowDate;
    return this;
  }

  /**
   * Get borrowDate
   * @return borrowDate
   **/
  @JsonProperty("borrowDate")
  @ApiModelProperty(value = "")
  public Date getBorrowDate() {
    return borrowDate;
  }

  public void setBorrowDate(Date borrowDate) {
    this.borrowDate = borrowDate;
  }


  @Override
  public boolean equals(java.lang.Object o) {
    if (this == o) {
      return true;
    }
    if (o == null || getClass() != o.getClass()) {
      return false;
    }
    BorrowedTapeDetails borrowedTapeDetails = (BorrowedTapeDetails) o;
    return Objects.equals(this.tape, borrowedTapeDetails.tape) &&
        Objects.equals(this.borrowerId, borrowedTapeDetails.borrowerId) &&
        Objects.equals(this.borrowerName, borrowedTapeDetails.borrowerName) &&
        Objects.equals(this.borrowDate, borrowedTapeDetails.borrowDate);
  }

  @Override
  public int hashCode() {
    return Objects.hash(tape, borrowerId, borrowerName, borrowDate);
  }


  @Override
  public String toString() {
    StringBuilder sb = new StringBuilder();
    sb.append("class BorrowedTapeDetails {\n");
    
    sb.append("    tape: ").append(toIndentedString(tape)).append("\n");
    sb.append("    borrowerId: ").append(toIndentedString(borrowerId)).append("\n");
    sb.append("    borrowerName: ").append(toIndentedString(borrowerName)).append("\n");
    sb.append("    borrowDate: ").append(toIndentedString(borrowDate)).append("\n");
    sb.append("}");
    return sb.toString();
  }

  /**
   * Convert the given object to string with each line indented by 4 spaces
   * (except the first line).
   */
  private String toIndentedString(java.lang.Object o) {
    if (o == null) {
      return "null";
    }
    return o.toString().replace("\n", "\n    ");
  }
}

