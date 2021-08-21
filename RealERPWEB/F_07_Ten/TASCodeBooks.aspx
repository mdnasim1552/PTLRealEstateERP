<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="TASCodeBooks.aspx.cs" Inherits="RealERPWEB.F_07_Ten.TASCodeBooks" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
              <div class="container moduleItemWrpper minheight">
                <div class="contentPart">
                    <div class="row">
                        
                           <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblItem" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                            <asp:TextBox ID="txtFilter" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnSrch" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrch_Click"  ><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblStep" runat="server" CssClass="smLbl_to" Text="Step :"></asp:Label>
                                            <asp:DropDownList ID="ddlGroupList" runat="server" CssClass=" ddlPage">

                                                <asp:ListItem>Main Group</asp:ListItem>
                                                <asp:ListItem>Sub Group</asp:ListItem>
                                                <asp:ListItem>Sub-2 Group</asp:ListItem>
                                                <asp:ListItem>Sub-3 Group</asp:ListItem>
                                                <asp:ListItem Selected="True">Details</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lbtnShowData" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lbtnShowData_Click">Show Data</asp:LinkButton>
                                        </div>
                                       
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </fieldset>
                          <asp:GridView ID="gvCodeBook" runat="server" 
                    AutoGenerateColumns="False" onpageindexchanging="gvCodeBook_PageIndexChanging" 
                    onrowcancelingedit="gvCodeBook_RowCancelingEdit" 
                    onrowediting="gvCodeBook_RowEditing" onrowupdating="gvCodeBook_RowUpdating" 
                    Width="16px" AllowPaging="True" PageSize="20"  CssClass="table table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" 
                                                    style="text-align: right" 
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' 
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:CommandField ShowEditButton="True" >
                                 <ItemStyle Font-Bold="True" />
                                </asp:CommandField>
                                   
                                <asp:TemplateField HeaderText="CompCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvComCod" runat="server" Height="16px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>' 
                                                    Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GroupCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfGrp" runat="server" Height="16px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infgrp")) %>' 
                                                    Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inf Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfCod" runat="server" Height="16px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod")) %>' 
                                                    Width="49px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvInfCod1" runat="server" BorderColor="Blue" 
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="16" 
                                                    style="font-weight: 700; text-align: center;" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod1")) %>' 
                                                    Width="110px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfCod1" runat="server" Height="16px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infcod1")) %>' 
                                                    Width="110px" style="text-align: center; font-weight: 700"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Details Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvInfDesc" runat="server" BorderColor="Blue" 
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="95" 
                                                    style="font-weight: 700; text-align: left;" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                    Width="220px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfDesc" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>' 
                                                    Width="220px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvInfDes2" runat="server" BorderColor="Blue" 
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="45" 
                                                    style="font-weight: 700; text-align: left;" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdes2")) %>' 
                                                    Width="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInfDes2" runat="server" Height="20px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdes2")) %>' 
                                                    Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FPS Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvUnitFPS" runat="server" BorderColor="Blue" 
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12" 
                                                    style="font-weight: 700; text-align: left;" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitfps")) %>' 
                                                    Width="55px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnitFPS" runat="server" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitfps")) %>' 
                                                    Width="58px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="StdQtyFPS">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvStdQtyF" runat="server" BorderColor="Blue" 
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" MaxLength="12" 
                                                    style="font-weight: 700; text-align: right;" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("###0.0000;(###0.0000); ") %>' 
                                                    Width="55px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvStdQtyF" runat="server" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdqtyf")).ToString("#,##0.0000;(#,##0.0000); ") %>' 
                                                    Width="58px" style="text-align: right"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project Name" Visible="false">
                                     <EditItemTemplate>
                                         <asp:Panel ID="Panel2" runat="server" BorderStyle="none"
                                             BorderWidth="1px">
                                             <table style="width: 100%;">
                                                 <tr>
                                                     <td>
                                                         <asp:TextBox ID="txtSerachProject" runat="server" BorderStyle="Solid" 
                                                             BorderWidth="1px" Height="18px" TabIndex="4" Width="80px"></asp:TextBox>
                                                     </td>
                                                     <td>
                                                         <asp:LinkButton ID="ibtnSrchProject" runat="server" Height="16px" 
                                                             CssClass="btn btn-primary srearchBtn" onclick="ibtnSrchProject_Click" TabIndex="5" 
                                                             Width="16px" ><span class="glyphicon glyphicon-search"></span></span></asp:LinkButton>
                                                     </td>
                                                     <td>
                                                         <asp:DropDownList ID="ddlProName" CssClass="form-control" runat="server" Width="250px" TabIndex="6">
                                                         </asp:DropDownList>
                                                     </td>
                                                 </tr>
                                             </table>
                                         </asp:Panel>
                                     </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProName" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc1")) %>' 
                                            Width="250px"></asp:Label>
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

