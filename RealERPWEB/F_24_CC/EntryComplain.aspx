<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryComplain.aspx.cs" Inherits="RealERPWEB.F_24_CC.EntryComplain" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                           <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Complain No:"></asp:Label>
                                        <asp:Label ID="lblCurCompNo1" runat="server" CssClass="inputTxt inputBox50px"></asp:Label>
                                        <asp:TextBox ID="txtCurCompNo2" runat="server" CssClass="inputTxt inputBox50px" TabIndex="3">000</asp:TextBox>

                                        <asp:Label ID="Label7" runat="server" CssClass=" smLbl_to" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurCompDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurCompDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurCompDate">
                                        </cc1:CalendarExtender>


                                         <asp:Label ID="Label9" runat="server" CssClass="smLbl_to" Text="REF No."></asp:Label>

                                        <asp:TextBox ID="txtComRef" runat="server" CssClass="inputTxt inputDateBox" TabIndex="3"></asp:TextBox>                                        
                                        <div class="col-md-3 pading5px pull-right">
                                        <asp:LinkButton ID="lbtnPrevCompList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevCompList_Click">Prev.Comp.List:</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevCompList" runat="server" CssClass=" ddlPage inputTxt" Width="130px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                    </div>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>


                                            <asp:TextBox ID="txtsrchproject" runat="server" CssClass=" inputtextbox" TabIndex="10"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="lbtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>


                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True" TabIndex="3" style="width:385px;" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:Label ID="lblddlProject" runat="server" Visible="False" style="width:385px;" CssClass=" smLbl_to inputTxt"></asp:Label>

                                       
                                    </div>
                                    
                                     <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="11">Ok</asp:LinkButton>

                                        </div>

                                   <%-- <div class ="from-group">--%>

                                        <div class="col-md-9 pading5px">
                                        <asp:Label ID="lblunit" runat="server" CssClass="lblTxt lblName" Text="Unit Name"></asp:Label>


                                            <asp:TextBox ID="txtunit" runat="server" CssClass=" inputtextbox" TabIndex="10"></asp:TextBox>

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="btnunitseach" CssClass="btn btn-primary srearchBtn" runat="server" onclick="btnunitseach_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>


                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True" TabIndex="3" style="width:385px;"></asp:DropDownList>
                                        <asp:Label ID="lblddlunit" runat="server" Visible="False" style="width:385px;" CssClass=" smLbl_to inputTxt"></asp:Label>

                                       
                                   <%-- </div>--%>


                                    </div>

                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg1" CssClass="btn btn-danger primaryBtn" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                                
                             <%--   <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:LinkButton ID="lbtnPrevISSList" runat="server" CssClass="lblTxt lblName prvLinkBtn" OnClick="lbtnPrevISSList_Click">Prev. Issue. List:</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass=" ddlPage inputTxt" Width="482px" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                                    </div>
                                </div>--%>
                            </div>
                        </fieldset>
                        </div>
                        <asp:GridView ID="gvComplain" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="431px" CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Complain No">
                                   <%-- <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            onclick="lbtnTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunit" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcod")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                            Font-Size="12px" onclick="lbtnUpdate_Click" CssClass="btn  btn-danger primarygrdBtn"> Update </asp:LinkButton>
                                    </FooterTemplate>  
            
                              </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Complain </br> Date">
                                                
                                      <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                            Font-Size="12px" onclick="lbtnUpdate_Click" CssClass="btn  btn-danger primarygrdBtn"> Update </asp:LinkButton>
                                    </FooterTemplate>                        
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcomdat" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "compdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:TextBox>

                                         <cc1:CalendarExtender ID="txtgvcomdat_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtgvcomdat"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                </asp:TemplateField>--%>

                                 <asp:TemplateField HeaderText="Complain Description">
                                    
                                    
                                     
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtComplain" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comdesc")) %>'
                                            Width="300px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->









        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



