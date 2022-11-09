<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LinkMktSurvey.aspx.cs" Inherits="RealERPWEB.F_12_Inv.LinkMktSurvey" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container moduleItemWrpper">
        <div class="contentPart">


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                                    <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3">


                                    <asp:Label ID="lblPreMrList" runat="server" CssClass="lblTxt lblName" Text="MSR List"></asp:Label>
                                    <asp:TextBox ID="txtPreMSRSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                    <div class="colMdbtn">
                                        <asp:LinkButton ID="ImgbtnFindPreMR" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindPreMR_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:DropDownList ID="ddlPrevMSRList" runat="server" CssClass="form-control inputTxt" TabIndex="6">
                                    </asp:DropDownList>

                                </div>

                                <div class="col-md-1 pading5px">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="2">Ok</asp:LinkButton>

                                </div>


                            </div>

                        </div>
                    </fieldset>
                    <asp:MultiView ID="Multiview1" runat="server">
                        <asp:View ID="SurveyInf1" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMSRInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="274px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRSuplCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Materials Description ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req. Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvMSRqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="55px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRSuplDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Concern  Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRCperson" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Telephone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRPhone" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRMobile" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMSRResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Purchase Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvLRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Price">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvMSRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Credit Period (Day)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvMSRPayment" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery Period (Day)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvMSRDel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delivery")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Limit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayLimit" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paylimit")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvbrand" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Bold="False" Font-Size="11px" Height="16px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brand")) %>'
                                                    Width="35px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvMSRRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "msrrmrk").ToString() %>' Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>
                        <asp:View ID="SurveyInf2" runat="server">
                            <div class="row" style="margin: 5px;">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvMSRInfo2" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True"
                                        OnRowDataBound="gvMSRInfo2_RowDataBound" OnRowCreated="gvMSRInfo2_RowCreated">
                                        <PagerSettings Visible="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" Materials Description ">

                                                <%-- <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary  primarygrdBtn">Total</asp:LinkButton>
                                        </FooterTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResDesc" runat="server"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                        Width="150px">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Requirement">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="55px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">
                                                <%--<FooterTemplate>
                                            <asp:LinkButton ID="lbtnMSRUpdate" runat="server" OnClick="lbtnMSRUpdate_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>
                                        </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtrate1" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Amount">


                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt1" runat="server" Width="70"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAmount1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>





                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtrate2" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Amount">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt2" runat="server" Width="70"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAmount2" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>



                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtrate3" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Amount">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt3" runat="server" Width="70"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAmount3" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>



                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtrate4" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">

                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFamt4" runat="server" Width="70"></asp:Label>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAmount4" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>



                                                <FooterStyle HorizontalAlign="RIght" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Previous App.rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaprovrate" runat="server"
                                                        Text='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovrate"))=="0.00")?"" :Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovrate"))  %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRRemarks" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "msrrmrk").ToString() %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row" style="margin: 5px;">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvterm" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True">
                                        <PagerSettings Visible="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" " Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSupplierdelete" runat="server"><span class="glyphicon glyphicon-remove" > </span></asp:LinkButton>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Suppliercode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvssircode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvDiscount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="55px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Carring Charge">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvccharge" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "ccharge").ToString() %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Quotation Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCurQuTDate" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"
                                                        Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"qutdate")).Year==1900?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"qutdate")).ToString("dd-MMM-yyyy")) %>'> </asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtCurQuTDate_CalendarExtender" runat="server"
                                                        TargetControlID="txtCurQuTDate"></cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Goodwill">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgoodwill" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "goodwill").ToString() %>'
                                                        Style="text-align: center; background-color: Transparent"
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pay Term/Cr Period">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvpayterm" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: center; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "payterm").ToString() %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material Availability">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtmatavailable" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "matavailable").ToString() %>'
                                                        Style="text-align: center; background-color: Transparent"
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delivery Condition">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtdelcon" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "delcon").ToString() %>'
                                                        Style="text-align: center; background-color: Transparent"
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AIT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtait" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "ait").ToString() %>'
                                                        Style="text-align: center; background-color: Transparent"
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Notes">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNotes" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "notes").ToString() %>'
                                                        Style="text-align: center; background-color: Transparent"
                                                        Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lead Time Required By Supplier">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtworkline" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "worktime").ToString() %>'
                                                        Style="text-align: center; background-color: Transparent"
                                                        Width="120px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </asp:View>
                    </asp:MultiView>

                    <fieldset class="scheduler-border fieldset_Nar">
                        <div class="form-horizontal">

                            <div class="form-group">
                                <div class="col-md-3 pading5px asitCol3 ">
                                    <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt lblName" Text="Approved By:" Visible="False"></asp:Label>
                                    <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt lblName" Text="Approv.Date" Visible="False"></asp:Label>
                                </div>
                                <div class="col-md-4 pading5px">

                                    <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt lblName" Text="Prepared By" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>

                                </div>

                            </div>
                            <div class="form-group">
                                <div class="col-md-6 pading5px">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txtMSRNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <asp:LinkButton ID="lbtnMSRUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnMSRUpdate_Click" OnClientClick="return confirm('Do you want to Link?')">Final Update</asp:LinkButton>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-2 pading5px asitCol2">
                                    <asp:TextBox ID="txtApprovedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                </div>
                                <div class="col-md-2 pading5px asitCol2">
                                    <asp:TextBox ID="txtApprovalDate" runat="server" class="form-control" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>
                                </div>
                                <div class="col-md-2 pading5px asitCol2">
                                    <asp:TextBox ID="txtPreparedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                </div>
                                </td>
                            </div>
                        </div>

                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
