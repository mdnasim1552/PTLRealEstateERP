<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccSpecificCodeBook.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccSpecificCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
          

        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
       


        };

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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                 <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="LblBookName" runat="server" CssClass="lblTxt lblName">Code Book</asp:Label>
                                        <asp:TextBox ID="txtSrchResouce" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        
                                         
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgSearchSrchResouce" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchSrchResouce_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlMainBook" runat="server" CssClass="form-control inputTxt chzn-select " style=" width:270px;">
                                        </asp:DropDownList>
                                      
                                        

                                    </div>

                                     <div class="col-md-3 pading5px ">
                                      <asp:DropDownList ID="ddlCodeSegment" runat="server"  CssClass="ddlPage" style="width:130px;">
                                                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                            <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                                            <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                                        </asp:DropDownList>
                                      
                                        
                                           <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                        </div>
                                    </div>
                                     
                                   

                                   <div class="col-md-3 pading5px pull-right">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" ></asp:Label>
                                       

                                    </div>
                                      
                                </div>

                                 <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="LblBookName2" runat="server" CssClass="lblTxt lblName">Group:</asp:Label>
                                        <asp:TextBox ID="txtsrch" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        
                                         
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnGroup" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnGroup_Click"  Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control inputTxt chzn-select" style="width:270px;">
                                        </asp:DropDownList>
                                        
                                        

                                    </div>

                                     <div class="col-md-3 pading5px ">

                                       
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnSrch_Click"  Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                          <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Visible="false">Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                          
                                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                                            CssClass="ddlPage">
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                            <asp:ListItem>30</asp:ListItem>
                                                            <asp:ListItem>50</asp:ListItem>
                                                            <asp:ListItem>100</asp:ListItem>
                                                            <asp:ListItem>150</asp:ListItem>
                                                            <asp:ListItem>200</asp:ListItem>
                                                            <asp:ListItem>300</asp:ListItem>
                                                            <asp:ListItem>600</asp:ListItem>
                                                            <asp:ListItem>900</asp:ListItem>
                                                        </asp:DropDownList>
                                        

                                    </div>
                                     
                                    

                                   
                                      
                                </div>
                                      
                               </div>
                       </fieldset>

                         <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" 
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="15" Width="499px"
                            OnPageIndexChanging="grvacc_PageIndexChanging"  CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvacc_RowDataBound">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                           
                            <Columns>

                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server" Visible="False"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod3")) %>'></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" " Visible="false">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server" Width="40px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod2"))+"-" %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Width="40px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod2"))+"-" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Spcf. Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Height="16px" MaxLength="6"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="60px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod4")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Width="60px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>

                               
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="250px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Style="font-size: 12px" Width="250px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                            </Columns>
                           
                              <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>




                    </div>
                </div>
                <!-- end of Content Part-->
            </div>   <!-- end of container Part-->

         











          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


