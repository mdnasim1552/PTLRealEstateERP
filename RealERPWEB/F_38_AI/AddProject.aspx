<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="RealERPWEB.F_38_AI.AddProject" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .chzn-select {
            width: 100%;
        }

        .chzn-container {
            width: 100% !important;
        }
      
        .gview tr td{
            border: 0;
        }
        .gview .form-control {
            height:25px;
            line-height: 25px;
            padding:0 12px;
            border-style: solid !important;
            border-color:#c6c9d5 !important;
        }
  
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>


             <div class="card mt-5">

                    <div class="card-header">


                    <div class="row">
                        <div class="col-md-4">
                            <div>
                                <h6>Project Entry</h6>
                            </div>
                        </div>
                        <div class="col-md-8">
                                <h6>Project List</h6>

                        <asp:LinkButton ID="tblAddCustomerModal" runat="server" CssClass="btn btn-primary ml-auto d-none  btn-sm mt20 mr-1 float-right" ><i class="fa fa-plus"></i>Add Customer</asp:LinkButton>

                        </div>
                    </div>
                  </div>
                    <div class="card-body">

                     <div class="row">
                        <div class="col-md-4">
                           
                             <div class="table-responsive">
                                    <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" CssClass="table-bordered gview"
                                        ShowFooter="False" ShowHeader="false" AllowPaging="false" Visible="True" Width="100%"  >
                                       
                                        <Columns>
                                    
                                            <asp:TemplateField HeaderText="Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                        Width="49px" ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgcResDesc1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                        Width="120px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvgval" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" >
                                                <ItemTemplate>
                                                    
                                                    <asp:TextBox ID="txtgvVal" runat="server" 
                                                        CssClass="form-control" BackColor="Transparent"
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>'>
                                                    </asp:TextBox>
                                                    <asp:TextBox ID="txtgvdVal" runat="server" AutoCompleteType="Disabled"
                                                        CssClass="form-control" BackColor="Transparent" 
                                                        BorderColor="#660033" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>'>
                                                    </asp:TextBox>

                                                    <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server" 
                                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal" PopupPosition="TopLeft" PopupButtonID="txtgvdVal"></cc1:CalendarExtender>
                                                 
                                                        <asp:DropDownList ID="ddlval" runat="server" Visible="false"
                                                                CssClass="select2 form-control" TabIndex="2">
                                                            </asp:DropDownList>

                                            
                                                    <asp:Label ID="lgvgdatat" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdatat")) %>' Width="250px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                               
                                                </ItemTemplate>
                                                <HeaderStyle Width="250" />
                                                <ItemStyle  Width="250" />
                                            </asp:TemplateField>
                                           
                                        </Columns>


                                  <FooterStyle CssClass="grvFooter" />
                               
                                                
                                <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    <asp:LinkButton ID="btnProjectSave" runat="server" OnClick="btnProjectSave_Click" CssClass="btn btn-primary  float-right">Project Save</asp:LinkButton>
                                </div>
                        </div>
                        <div class="col-md-8">
                          
                                <div class="table-responsive">
                                    <asp:GridView ID="GridcusDetails" runat="server" AutoGenerateColumns="False" Width="100%" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Visible="True" AllowPaging="true" PageSize="15">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL # ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right; font-size: 12px;"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                                        ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinfdesc" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                         ForeColor="Black"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="Phone Number" >
                                                <ItemTemplate>
                                                    <asp:Label ID="tbldesc" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Address" >
                                                <ItemTemplate>
                                                    <asp:Label ID="tbladdress" runat="server"  Width="120px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "address1")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Country" >
                                                <ItemTemplate>
                                                    <asp:Label ID="tblcountry" runat="server"  Width="120px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "country")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" >
                                                <ItemTemplate>
                                                  <asp:LinkButton ID="lnkView" runat="server" CssClass="text-primary pr-2 pl-2"><i class="fa fa-eye"></i></asp:LinkButton>

                                                <asp:LinkButton ID="btnRemove" runat="server"  CssClass="text-danger pr-2"><i class="fa fa-trash"></i></asp:LinkButton>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="text-primary" ><i class="fa fa-edit"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>


                                        <%--<FooterStyle CssClass="grvFooter" />--%>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />

                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                          </div>
                            </div>
                        </div>

                   
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
