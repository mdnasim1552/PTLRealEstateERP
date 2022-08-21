<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpEvaCode.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_91_ACR.EmpEvaCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    

      <style>



                .mt20 {
            margin-top: 20px;
        }

            .chzn-container{
             width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 35px !important;
        }

        .card-body {
            min-height: 400px !important;
        }
                  .pd4{
                    padding:4px!important;
                }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        $('#<%=this.gvAcrCBook.ClientID %>').tblScrollable();

            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>


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
                <div class="contentPart">
                     <div class="card-header">
                    <div class="row">
                        <div class="col-lg-4">
                       
                       <%-- <fieldset class="scheduler-border">--%>

                            <%--<div class="form-horizontal">--%>
                                <div class="form-group">
                                    
                                     
                                          <asp:Label ID="LblBookName1" runat="server" CssClass="control-label lblTxt" Text="Select Code Book:"></asp:Label>
                                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>

                                          <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                        <%--                                                <cc1:ListSearchExtender ID="ddlCodeBook_ListSearchExtender" runat="server"
                                                    Enabled="True" QueryPattern="Contains" TargetControlID="ddlCodeBook">
                                                </cc1:ListSearchExtender>--%>
                                
                                    </div>
                            </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                         <asp:Label ID="LblDetails" runat="server" CssClass="control-label lblTxt" Text="Details Code:"></asp:Label>
                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    </div>
                                    <div class="col-lg-1 ">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary  mt20"></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 ">
                                        <div class="msgHandSt">
                                            <asp:Label ID="ConfirmMessage" CssClass="btn-danger  " runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    
                                </div>

                           
                            </div>
                          <%--  </div>--%>
                     <%--   </fieldset>--%>
                            </div>

                <div class="card-body">

                        <asp:GridView ID="gvAcrCBook" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" 
                            OnRowCancelingEdit="gvAcrCBook_RowCancelingEdit" OnRowEditing="gvAcrCBook_RowEditing"
                            OnRowUpdating="gvAcrCBook_RowUpdating" Width="726px" ShowFooter="True" CssClass="table table-striped table-hover table-bordered grvContentarea" PageSize="30">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                Visible="False" />
                            <FooterStyle/>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                            MaxLength="3"
                                            Style="border-style: none; border-color: midnightblue; font-size: 12px; text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3"))  %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                            Width="50px" Style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                       
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                             <asp:TemplateField HeaderText="Details">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvdDesc" runat="server" Font-Size="12px" MaxLength="100" TextMode="MultiLine"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ddesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        
                                           <asp:TextBox ID="lblddesc" runat="server" Font-Size="12px" MaxLength="100" TextMode="MultiLine"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ddesc")) %>'
                                            Width="250px"></asp:TextBox>
                                      <%--  <asp:Label ID="lblddesc" runat="server" Font-Size="12px" 
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ddesc")) %>'
                                            Width="250px"></asp:Label>--%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
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

