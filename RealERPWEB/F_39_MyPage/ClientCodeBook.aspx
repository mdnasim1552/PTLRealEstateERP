<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ClientCodeBook.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.ClientCodeBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%--<table style="width: 910px; height: 30px;">
        <tr>
            <td class="style43">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Italic="False" 
                    Font-Size="25px" ForeColor="Navy" Height="20px" Text="SCHEDULE &amp; OTHERS CODE BOOK INFORMATION INPUT/EDIT SCREEN" 
                    Width="639px" BackColor="#9999FF" style="font-size: 18px"></asp:Label>
            </td>
            <td class="style47">
                &nbsp;</td>
            <td class="style44">
                        <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
                    </td>
            <td>
                <asp:LinkButton ID="lnkPrint" runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Size="12px" Font-Underline="True" Height="17px" 
                    onclick="lnkPrint_Click" 
                    
                    style="  border-left-width: 2px; border-left-color: #ffff00;   text-align: center; font-size: 12px;" 
                    CssClass="button">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>--%>


<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">


                        <fieldset class="scheduler-border">

                            <div class="form-horizontal">


                                <div class="form-group">

                                    <div class="col-md-5 pading5px">

                                         <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to" Text="Page Size"> </asp:Label>
                                     
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" ddlPage"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                                <asp:ListItem>150</asp:ListItem>
                                                <asp:ListItem>200</asp:ListItem>
                                                <asp:ListItem>300</asp:ListItem>
                                                <asp:ListItem>600</asp:ListItem>
                                                <asp:ListItem Selected="True">900</asp:ListItem>
                                            </asp:DropDownList>
                                        <asp:Label ID="LblBookName2" runat="server" CssClass=" smLbl_to" Text="Search Option:"></asp:Label>                                       
                                        <asp:TextBox ID="txtsrch" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        
                                       
                                          </div>
                                        <div class="col-md-4 pading5px">
                                            <div class="msgHandSt">
                                                <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>

                                  

                                </div>

                            </div>
                        </fieldset>
                      
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="900" OnPageIndexChanging="grvacc_PageIndexChanging"
                                OnRowDataBound="grvacc_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <FooterStyle BackColor="#5F9467" />

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True">
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText=" " >

                                        <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgrcode" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))  %>'
                                            Width="50px">                                             
                                            
                                        </asp:HyperLink>


                                           
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>







                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                                MaxLength="3"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                Width="250px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label8" runat="server" Text="Description of Code" Width="150px"></asp:Label>


                                        </HeaderTemplate>
                                        <ItemTemplate>



                                            <asp:HyperLink ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))  %>'
                                                Width="250px">                                             
                                            
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                   
                                  
                                  
                                   

                                     <asp:TemplateField HeaderText="Address">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvaddress" runat="server" 
                                                        style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>' 
                                                        Width="150px"></asp:TextBox>
                                                </EditItemTemplate>
                                                
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvaddress" runat="server" style="FONT-SIZE: 12px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "caddress")) %>' 
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Phone">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvphone" runat="server" 
                                                        style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>' 
                                                        Width="160px"></asp:TextBox>
                                                </EditItemTemplate>
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvphone" runat="server" style="FONT-SIZE: 12px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>' 
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Email">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvemail" runat="server" 
                                                        style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>' 
                                                        Width="150px"></asp:TextBox>
                                                </EditItemTemplate>
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvemail" runat="server" style="FONT-SIZE: 12px" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>' 
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Image">
                                         <EditItemTemplate>
                                              
                                <asp:FileUpload ID="uplFile" runat="server" Height="30px" Width="30px"  CssClass="txtCss"  />
                                      <asp:Label ID="lblimg01" runat="server" Text='<%#Eval("IMGPATH") %>' Width="60px" Height="16px" Visible="false"></asp:Label> 
                                    </EditItemTemplate>
                                        <ItemTemplate>
                                               <asp:Image ID="lblImageUrl" Width="50" Height="50" runat="server" ImageUrl='<%#Eval("IMGPATH") %>' class="img-responsive" ></asp:Image>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Client Status">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkactive" runat="server" 
                                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active"))=="True" %>' 
                                                            Width="20px" />
                                                    </EditItemTemplate>
                                                     <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" style="FONT-SIZE: 12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active")) %>' 
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                    
                                </Columns>


                                <RowStyle />
                                <EditRowStyle />
                                <SelectedRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <AlternatingRowStyle BackColor="" />
                            </asp:GridView>
                          
                       
                    </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>


</asp:Content>


