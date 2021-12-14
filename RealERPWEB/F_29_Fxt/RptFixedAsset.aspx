<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptFixedAsset.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.RptFixedAsset" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                   <%-- <a href="RptFixedAsset.aspx">RptFixedAsset.aspx</a>--%>
                                    <div class="col-md-8 pading5px asitCol8">
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true">
                                        </cc1:CalendarExtender>


                                        <asp:Label ID="lbltoDate" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true">
                                        </cc1:CalendarExtender>
                                         <asp:Label ID="lblcal" runat="server" CssClass="smLbl_to" Text="Calculation Date :"></asp:Label>
                                          <asp:TextBox ID="txtcal" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtcal_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtcal" Enabled="true">
                                        </cc1:CalendarExtender>

                               <%--    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkdetail_Click">Ok</asp:LinkButton>--%>


                                  </div>
                                    
                                        <%--<div class="col-md-1">
                                            <asp:LinkButton ID="lnkdetail" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkdetail_Click">Ok</asp:LinkButton>

                                        </div>
                                   --%>
                                </div>
                            </div>
                        </fieldset>
                      <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                
                                    <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName"  runat="server" Width="233" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDeptDesc" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccResCode" runat="server" CssClass="lblTxt lblName" Text="Material"></asp:Label>
                                        <asp:TextBox ID="txtSrchmat" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindlist" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindlist_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    

                                    <div class="col-md-3 pading5px  asitCol5">
                                        <asp:DropDownList ID="ddllMatList" runat="server" Width="300px" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                        </div>

                            

                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lnkdetail" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkdetail_Click">Ok</asp:LinkButton>

                                    </div>




                                </div>

                                </div>

                            </div>


                        </fieldset>


                    </div>
                 
                        
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvFixAsst" runat="server" AutoGenerateColumns="False"
                                        CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" >
                                        <Columns>
                                                      <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                                            
                           <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblAsst" runat="server" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>


                            <asp:TemplateField HeaderText=" Floor">
                                <ItemTemplate>

                                    <asp:Label ID="txtfloor" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "floorno")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                              
                                </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText=" User Name">
                                
                                <ItemTemplate>
                                    
                                    <asp:Label ID="lblusrname" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                        Width="170px"></asp:Label>
                                </ItemTemplate>
                               
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                           
                       <%--   <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lbldig" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField> --%>
                                    
                                    
                                      <asp:TemplateField HeaderText="Model#">
                                <ItemTemplate>
                                    <asp:Label ID="lblmodel" runat="server" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "modelno")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Asset id </br> code">
                                <ItemTemplate>
                                    <asp:Label ID="lblidcode" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assetidcode")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 
                             <asp:TemplateField HeaderText="Date of</br>Purchase">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvdate" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "purchasedate")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Estimated Life">
                                <ItemTemplate>
                                    <asp:Label ID="lblestimated" runat="server" BorderStyle="none" 
                                       
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "estimatedlife")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>

                              

                            <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:Label ID="lblcategory" runat="server" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Purchase Price">
                                <ItemTemplate>
                                    <asp:Label ID="txtpurprice" runat="server" BorderStyle="none" style="text-align:right"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purchaseprice")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                               <asp:TemplateField HeaderText="Rate of Depreciation">
                                <ItemTemplate>
                                    <asp:Label ID="txtrate" runat="server" BorderStyle="none" 
                                        Text='<%#(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate"))==0.00)? Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); "): Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") + "%"  %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Accumulated  Depreciation">
                                <ItemTemplate>
                                    <asp:Label ID="txtAccmulated" runat="server" BorderStyle="none" style="text-align:right" 
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "accudepreciation")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of</br> Depreciation">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvddate" runat="server" BorderStyle="none"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "depreciationdate")).ToString("dd-MMM-yyyy") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            <asp:TemplateField HeaderText="Current year Depreciation">
                                <ItemTemplate>
                                    <asp:Label ID="txtcurrent" runat="server" BorderStyle="none"  style="text-align:right"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "currentyear")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Adjustment /Disposal">
                                <ItemTemplate>
                                    <asp:Label ID="txtadjustment" runat="server" BorderStyle="none"  style="text-align:right"  
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjustment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>

                             <asp:TemplateField HeaderText="Wdv">
                                <ItemTemplate>
                                    <asp:Label ID="txtwdv" runat="server" BorderStyle="none" style="text-align:right"   
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wdv")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Warranty">
                             <ItemTemplate>
                                    <asp:Label ID="lblwarranty" runat="server" BorderStyle="none" style="text-align:right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "warranty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblremarks" runat="server" BorderStyle="none" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
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


